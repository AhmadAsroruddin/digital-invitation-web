using AutoMapper;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class CheckInService(ICheckInRepository checkinRepository, IRSPVRepository rSPVRepository,IMapper mapper) : ICheckInService
    {
        private readonly ICheckInRepository checkinRepository = checkinRepository;
        private readonly IRSPVRepository rSPVRepository = rSPVRepository;
        private readonly IMapper mapper = mapper;

        public async Task<CheckInResponse> CreateAsync(SaveCheckInRequest request)
        {
            var checkin = mapper.Map<Checkin>(request);

            await checkinRepository.CreateAsync(checkin);

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