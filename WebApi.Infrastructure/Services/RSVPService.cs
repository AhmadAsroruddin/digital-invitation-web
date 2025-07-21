using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApi.Shared;

namespace WebApi.Infrastructure.Services
{
    public class RSVPService(
        IRSPVRepository rSPVRepository,
        IGuestSubEventRepository guestSubEventRepository,
        IMapper mapper,
        IHubContext<GuestListHub> hubContext
        ) : IRSVPService
    {
        private readonly IRSPVRepository _rSPVRepository = rSPVRepository;
        private readonly IGuestSubEventRepository _guestSubEventRepository = guestSubEventRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHubContext<GuestListHub> _hubContext = hubContext;

        public async Task<RSVPResponse> CreateAsync(int guestSubEventId, SaveRSVPRequest request)
        {
            var exists = await _rSPVRepository.GetOneAsync(x => x.GuestSubEventId == guestSubEventId);
            if (exists != null)
            {
                throw new InvalidOperationException("RSVP failed: guest has already submitted a response.");
            }

            var guestSubEvent = await _guestSubEventRepository.GetOneAsync(
                e => e.Id == guestSubEventId,
                includeProperties: ["SubEvent", "SubEvent.Event"])
                ?? throw new InvalidOperationException("Guest Sub event not found");

            var rsvp = _mapper.Map<RSVP>(request);
            rsvp.GuestSubEventId = guestSubEventId;
            rsvp.RSVPTime = DateTime.Now;
            rsvp.Status = ParseRSVPStatus(request.Status);

            if (request.PaxConfirmed > (guestSubEvent.SubEvent?.MaxPax ?? int.MaxValue))
                throw new InvalidProgramException("RSVP failed: participant quota has been reached.");

            await _rSPVRepository.CreateAsync(rsvp);

            var rsvpResponse = _mapper.Map<RSVPResponse>(rsvp);
            var subEventId = guestSubEvent.SubEvent?.Id;

            await _hubContext.Clients.Group($"event_{subEventId}")
                .SendAsync("EventEntityChanged", new
                {
                    type = "RSVP_UPDATED",
                    entity = rsvpResponse
                });

            return rsvpResponse;
        }

        public Task<bool> DeletedAsync(int RSVPId, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RSVPResponse>> GetAllAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<RSVPResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RSVPResponse> UpdateAsync(int id, SaveRSVPRequest request)
        {
            throw new NotImplementedException();
        }

        private static RSVPStatus ParseRSVPStatus(string status)
        {
            var normalized = status.Replace(" ", "").Replace("_", "").ToLower();

            return normalized switch
            {
                "attending" => RSVPStatus.Attending,
                "notattending" => RSVPStatus.NotAttending,
                _ => throw new ArgumentException(
                    $"Invalid RSVP status: '{status}'. Allowed values: 'attending', 'not_attending'.")
            };
        }

    }
}
