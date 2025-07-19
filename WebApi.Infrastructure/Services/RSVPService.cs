using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
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
            // Cek apakah sudah pernah RSVP
            var exists = await _rSPVRepository.GetOneAsync(x => x.Id == guestSubEventId);
            if (exists != null)
                throw new InvalidOperationException("Guest already do RSVP");

            // Ambil detail sub-event beserta Event parent-nya
            var guestSubEvent = await _guestSubEventRepository.GetOneAsync(
                e => e.Id == guestSubEventId,
                includeProperties: ["SubEvent", "SubEvent.Event"])
                ?? throw new InvalidOperationException("Guest Sub event not found");

            // Mapping RSVP dari request, assign GuestSubEventId dan waktu RSVP
            var rsvp = _mapper.Map<RSVP>(request);
            rsvp.GuestSubEventId = guestSubEventId;
            rsvp.RSVPTime = DateTime.Now;

            // Cek kuota
            if (request.PaxConfirmed > (guestSubEvent.SubEvent?.MaxPax ?? int.MaxValue))
                throw new InvalidProgramException("RSVP failed: participant quota has been reached.");

            // Simpan RSVP
            await _rSPVRepository.CreateAsync(rsvp);

            // Mapping response
            var rsvpResponse = _mapper.Map<RSVPResponse>(rsvp);
            var eventId = guestSubEvent.SubEvent!.EventId;

            // --- [ SIGNALR BROADCAST KE GROUP SESUAI EVENT ] ---
            await _hubContext.Clients.Group($"event_{eventId}").SendAsync("RSVPUpdated", rsvpResponse);

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
    }
}
