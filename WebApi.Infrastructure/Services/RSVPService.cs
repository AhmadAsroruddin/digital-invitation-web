using AutoMapper;
using Microsoft.Identity.Client;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class RSVPService(IRSPVRepository rspvRepository, IGuestSubEventRepository guestSubEventRepository, IMapper mapper) : IRSVPService
    {
        private readonly IRSPVRepository rSPVRepository = rspvRepository;
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IMapper mapper = mapper;

        public async Task<RSVPResponse> CreateAsync(int guestSubEventId, SaveRSVPRequest request)
        {
            var exists = await rSPVRepository.GetOneAsync(x => x.Id == guestSubEventId);
            
            if (exists != null)
            {
                throw new InvalidOperationException("Guest already do RSVP");
            }
            
            var guestSubEvent = await guestSubEventRepository.GetOneAsync(e => e.Id == guestSubEventId, includeProperties: ["SubEvent"])?? throw new InvalidOperationException("Guest Sub event not found");

            var rsvp = mapper.Map<RSVP>(request);
            rsvp.GuestSubEventId = guestSubEventId;
            rsvp.RSVPTime = DateTime.Now;

            var quotaConfirmation = request.PaxConfirmed > guestSubEvent.SubEvent?.MaxPax;
            if (quotaConfirmation)
            {
                throw new InvalidProgramException("RSVP failed: participant quota has been reached.");
            }

            await rSPVRepository.CreateAsync(rsvp);
            return mapper.Map<RSVPResponse>(rsvp);
        }

        public Task<bool> DeletedAsync(int RSVPId, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RSVPResponse>> GetAllAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<SubEventResponse> GetAllBySubEvent(int subEventId)
        {
            var rsvp = await rSPVRepository.GetAllAsync(e => e.GuestSubEvent!.SubEvent!.Id == subEventId, includeProperties: ["GuestSubEvent.SubEvent"]);

            var subEventResponse = mapper.Map<SubEventResponse>(rsvp.FirstOrDefault()!.GuestSubEvent!.SubEvent);
            subEventResponse.RSVPs = mapper.Map<List<RSVPResponse>>(rsvp);
            return subEventResponse;
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