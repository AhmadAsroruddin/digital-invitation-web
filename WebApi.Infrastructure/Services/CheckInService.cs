using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Shared;

namespace WebApi.Infrastructure.Services
{
    public class CheckInService(ICheckInRepository checkinRepository, IRSPVRepository rSPVRepository, IHubContext<GuestListHub> hubContext,IMapper mapper) : ICheckInService
    {
        private readonly ICheckInRepository checkinRepository = checkinRepository;
        private readonly IRSPVRepository rSPVRepository = rSPVRepository;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<GuestListHub> _hubContext = hubContext;

        public async Task<CheckInResponse> CreateAsync(SaveCheckInRequest request)
        {
            var checkin = mapper.Map<Checkin>(request);
            checkin.CheckinTime = DateTime.Now;
            checkin.CheckedIn = true;

            await checkinRepository.CreateAsync(checkin);
            var checkinSaved = await checkinRepository.GetOneAsync(e => e.CheckinId == checkin.CheckinId, includeProperties: ["GuestSubEvent.SubEvent"]);
            var subEventId = checkinSaved?.GuestSubEvent?.SubEventId;
            
            var checkInResponse = mapper.Map<CheckInResponse>(checkin);

            await _hubContext.Clients.Group($"event_{subEventId}")
                .SendAsync("EventEntityChanged", new
                {
                    type = "CHECKIN_UPDATED",
                    entity = checkInResponse
                });

            return mapper.Map<CheckInResponse>(checkin);
        }

        public async Task<IList<CheckInResponse>> GetAllBySubEventAsync(int subEventId)
        {
            var checkin = await checkinRepository.GetAllAsync(e => e.GuestSubEvent!.SubEventId == subEventId, includeProperties: ["GuestSubEvent.SubEvent"]);

            return mapper.Map<List<CheckInResponse>>(checkin);
        }

        public async Task<CheckInResponse> GetByIdAsync(int id)
        {
            var rsvp = await rSPVRepository.GetOneAsync(e => e.GuestSubEventId == id);
            var checkin = await checkinRepository.GetOneAsync(e => e.GuestSubEventId == id);

            if (rsvp != null && checkin == null)
            {
                throw new InvalidOperationException("The Guest not check-in yet");
            }

            if (rsvp == null && checkin == null)
            {
                throw new InvalidOperationException("Not guest found");
            }

            return mapper.Map<CheckInResponse>(checkin);
        }
    }
}