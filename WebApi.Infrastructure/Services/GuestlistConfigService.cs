using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApi.Shared;


namespace WebApi.Infrastructure.Services
{
    public class GuestlistConfigService(IGuestlistConfigRespository guestlistConfigRespository, IGuestSubEventRepository guestSubEventRepository , IRSPVRepository rSPVRepository, ICheckInRepository checkInRepository, IHubContext<GuestListHub> hubContext,IMapper mapper) : IGuestlistConfigService
    {
        private readonly IGuestlistConfigRespository guestlistConfigRespository = guestlistConfigRespository;
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IRSPVRepository rSPVRepository = rSPVRepository;
        private readonly ICheckInRepository checkInRepository = checkInRepository;
        private readonly IHubContext<GuestListHub> _hubContext = hubContext;
        private readonly IMapper mapper = mapper;

        public async Task<GuestlistConfigResponse> CreateAsync(SaveGuestlistConfigRequest request)
        {
            var guestlistConfig = mapper.Map<GuestlistConfig>(request);
            guestlistConfig.ShareCode = Guid.NewGuid().ToString("N");
            guestlistConfig.FilterJson = JsonSerializer.Serialize(request.FilterJson);
            guestlistConfig.ColumnsJson = JsonSerializer.Serialize(request.ColumnsJson);

            await guestlistConfigRespository.CreateAsync(guestlistConfig);

            return mapper.Map<GuestlistConfigResponse>(guestlistConfig);
        }

        public async Task<bool> DeleteById(int id)
        {
            var guestlistConfig = await guestlistConfigRespository.GetOneAsync(e => e.Id == id) ?? throw new NotFoundException("Guest List Configuration");

            await guestlistConfigRespository.DeleteAsync(guestlistConfig);

            return true;
        }

        public async Task<List<GuestlistConfigResponse>> GetByEventIdAsync(int eventId)
        {
            var guestlistConfig = await guestlistConfigRespository.GetAllAsync(e => e.SubEvent!.EventId == eventId, includeProperties: ["SubEvent"])?? throw new NotFoundException("Guest List Configuration");

            return mapper.Map<List<GuestlistConfigResponse>>(guestlistConfig);
        }

        public async Task<GuestlistConfigResponse> GetByIdAsync(int Id)
        {
            var guestlistConfig = await guestlistConfigRespository.GetOneAsync(e => e.Id == Id, includeProperties: ["SubEvent.Event"]) ?? throw new NotFoundException("Guest List Configuration");
            
            var response = mapper.Map<GuestlistConfigResponse>(guestlistConfig);
            response.EventId = guestlistConfig.SubEvent!.EventId;
            return response;
        }

        public async Task<GuestlistFilteredResponse> GetByShareCodeAsync(string shareCode)
        {
            var config = await guestlistConfigRespository.GetOneAsync(e => e.ShareCode == shareCode, includeProperties: ["SubEvent", "SubEvent.Event"]) ?? throw new InvalidOperationException("No Configuration Found");

            var filters = JsonSerializer.Deserialize<Dictionary<string, string>>(config.FilterJson);
            var columns = JsonSerializer.Deserialize<List<string>>(config.ColumnsJson);

            var guest = await guestSubEventRepository.GetAllAsync(e => e.SubEvent!.Id == config.SubEventId, includeProperties: [ "RSVP", "Checkin", "SubEvent", "SubEvent.Event","Guest"]);

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.Key == "InvitedBy" && filter.Value != "")
                    {
                        guest = guest.Where(e => e.Guest!.InvitedBy == filter.Value);
                    }
                    if (filter.Key == "GuestGroup" && filter.Value != "")
                    {
                        guest = guest.Where(e => e.Guest!.GuestGroup == filter.Value);
                    }
                    if (filter.Key == "SubEvent" && filter.Value != "")
                    {
                        guest = guest.Where(g => g.SubEventId == int.Parse(filter.Value));
                    }
                    if (filter.Key == "RSVP" && !string.IsNullOrWhiteSpace(filter.Value))
                    {
                        var value = filter.Value.Trim().ToLower();

                        if (value == "all")
                        {
                            guest = guest.Where(e => e.RSVP != null);
                        }
                        else
                        {
                            var normalized = value.Replace(" ", "").Replace("_", "");

                            if (Enum.TryParse<RSVPStatus>(normalized, ignoreCase: true, out var status))
                            {
                                guest = guest.Where(e => e.RSVP != null && e.RSVP.Status == status);
                            }
                            else
                            {
                                guest = guest.Where(e => false);
                            }
                        }
                    }
                }
            }

            var response = new GuestlistFilteredResponse
            {
                ConfigurationName = config.Name,
                FilterJson = config.FilterJson,
                ColumnsJson = config.ColumnsJson,
                SubEvent = mapper.Map<SubEventResponse>(config.SubEvent),
                Event = mapper.Map<EventResponse>(guest.FirstOrDefault()?.SubEvent?.Event),
                Guests = [.. guest.Select(g => {
                    return new GuestInList
                    {
                        GuestId = g.Id,
                        Name = g.Guest!.Name,
                        Phone = g.Guest!.Phone,
                        GuestGroup = g.Guest.GuestGroup,
                        InvitedBy = g.Guest.InvitedBy,
                        Pax = g.Guest.Pax,
                        SubEvents = g.Guest.GuestSubEvents?.Select(s => new SubEventResponse
                        {
                            Id = s.SubEvent!.Id,
                            Name = s.SubEvent.Name!,
                            StartTime = s.SubEvent.StartTime,
                            EndTime = s.SubEvent.EndTime,
                            Location = s.SubEvent.Location
                        }).ToList() ?? [],
                        RSVP = mapper.Map<RSVPResponse>(g.RSVP),
                        CheckIn = mapper.Map<CheckInResponse>(g.Checkin)
                    };
                })]
                
            };
            return response;
        }

        public async Task<GuestlistConfigResponse> UpdateAsync(int gueslistConfigId, SaveGuestlistConfigRequest request)
        {
            var guestlistConfig = await guestlistConfigRespository.GetByIdAsync(gueslistConfigId) ?? throw new NotFoundException("Guest List Configuration");

            mapper.Map(request, guestlistConfig);

            guestlistConfig.FilterJson = JsonSerializer.Serialize(request.FilterJson);
            guestlistConfig.ColumnsJson = JsonSerializer.Serialize(request.ColumnsJson);
            await guestlistConfigRespository.UpdateAsync(guestlistConfig);

            await _hubContext.Clients.Group($"event_{guestlistConfig.SubEventId}")
                .SendAsync("EventEntityChanged", new
                {
                    type = "RSVP_UPDATED",
                    entity = guestlistConfig
                });

            return mapper.Map<GuestlistConfigResponse>(guestlistConfig);
        }
    }
}