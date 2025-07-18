using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;


namespace WebApi.Infrastructure.Services
{
    public class GuestlistConfigService(IGuestlistConfigRespository guestlistConfigRespository, IGuestRepository guestRepository ,IMapper mapper) : IGuestlistConfigService
    {
        private readonly IGuestlistConfigRespository guestlistConfigRespository = guestlistConfigRespository;
        private readonly IGuestRepository guestRepository = guestRepository;
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
            var guestlistConfig = await guestlistConfigRespository.GetOneAsync(e => e.Id == id, includeProperties: ["Event"]) ?? throw new NotFoundException("Guest List Configuration");

            await guestlistConfigRespository.DeleteAsync(guestlistConfig);

            return true;
        }

        public async Task<List<GuestlistConfigResponse>> GetByEventIdAsync(int eventId)
        {
            var guestlistConfig = await guestlistConfigRespository.GetAllAsync(e => e.EventId == eventId, includeProperties: ["Event"])?? throw new NotFoundException("Guest List Configuration");

            return mapper.Map<List<GuestlistConfigResponse>>(guestlistConfig);
        }

        public async Task<GuestlistConfigResponse> GetByIdAsync(int Id)
        {
            var guestlistConfig = await guestlistConfigRespository.GetOneAsync(e => e.Id == Id, includeProperties: ["Event"]) ?? throw new NotFoundException("Guest List Configuration");

            return mapper.Map<GuestlistConfigResponse>(guestlistConfig);
        }

        public async Task<GuestlistFilteredResponse> GetByShareCodeAsync(string shareCode)
        {
            var config = await guestlistConfigRespository.GetOneAsync(e => e.ShareCode == shareCode, includeProperties: ["Event"]) ?? throw new InvalidOperationException("No Configuration Found");

            var filters = JsonSerializer.Deserialize<Dictionary<string, string>>(config.FilterJson);
            var columns = JsonSerializer.Deserialize<List<string>>(config.ColumnsJson);

            var guest = await guestRepository.GetAllAsync(e => e.EventId == config!.EventId, includeProperties: ["GuestSubEvents.SubEvent", "Event", "RSVPs", "Checkins", "GuestSubEvents"]);
            Console.WriteLine(guest.First().RSVPs);
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.Key == "InvitedBy" && filter.Value != "")
                    {
                        guest = guest.Where(e => e.InvitedBy == filter.Value);
                    }
                    if (filter.Key == "GuestGroup" && filter.Value != "")
                    {
                        guest = guest.Where(e => e.GuestGroup == filter.Value);
                    }
                    if (filter.Key == "SubEvent" && filter.Value != "")
                    {
                        guest = guest.Where(g => g.GuestSubEvents!.Any(gse => gse.SubEventId.ToString() == filter.Value));
                    }
                    if (filter.Key == "RSPV" && filter.Value != "")
                    {
                        if (filter.Value.Equals("all", StringComparison.CurrentCultureIgnoreCase))
                        {
                            guest = guest.Where(e => e.RSVPs != null && e.RSVPs.Count > 0);
                        }
                        else
                        {
                            guest = guest.Where(e => e.RSVPs != null && e.RSVPs.Any(rsvp => rsvp.Status == filter.Value));
                        }
                    }
                }
            }
            var response = new GuestlistFilteredResponse
            {
                ConfigurationName = config.Name,
                EventId = config.EventId,
                Event = config.Event!,
                FilterJson = config.FilterJson,
                ColumnsJson = config.ColumnsJson,
                Guests = [.. guest.Select(g => new GuestInList
                {
                    GuestId = g.Id,
                    Name = g.Name,
                    Phone = g.Phone,
                    GuestGroup = g.GuestGroup,
                    InvitedBy = g.InvitedBy,
                    Pax = g.Pax,
                    SubEvents = g.GuestSubEvents?.Select(s => new SubEventResponse
                    {
                        Id = s.SubEvent!.Id,
                        Name = s.SubEvent.Name!,
                        StartTime = s.SubEvent.StartTime,
                        EndTime = s.SubEvent.EndTime,
                        Location = s.SubEvent.Location
                    }).ToList() ?? [],
                    RSVPs = g.RSVPs?.Select(r => new RSVPResponse
                    {
                        Id = r.Id,
                        Status = r.Status,
                        PaxConfirmed = r.PaxConfirmed,
                        RSVPTime = r.RSVPTime
                    }).ToList() ?? []
                })]
                
            };
            return response;
        }

        public async Task<GuestlistConfigResponse> UpdateAsync(int gueslistConfigId, SaveGuestlistConfigRequest request)
        {
            var guestlistConfig = await guestlistConfigRespository.GetByIdAsync(gueslistConfigId) ?? throw new NotFoundException("Guest List Configuration");
            request.EventId = guestlistConfig.EventId;

            mapper.Map(request, guestlistConfig);

            guestlistConfig.FilterJson = JsonSerializer.Serialize(request.FilterJson);
            guestlistConfig.ColumnsJson = JsonSerializer.Serialize(request.ColumnsJson);
            await guestlistConfigRespository.UpdateAsync(guestlistConfig);

            return mapper.Map<GuestlistConfigResponse>(guestlistConfig);
        }
    }
}