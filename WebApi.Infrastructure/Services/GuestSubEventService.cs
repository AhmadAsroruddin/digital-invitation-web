using AutoMapper;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class GuestSubEventService(IGuestSubEventRepository guestSubEventRepository, IMapper mapper) : IGuestSubEventService
    {
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IMapper mapper = mapper;

        public async Task<GuestSubEventResponse> CreateAsync(int subEventId, SaveGuestSubEventRequest request)
        {
            var guestSubEvent = mapper.Map<GuestSubEvent>(request);
            guestSubEvent.SubEventId = subEventId;

            await guestSubEventRepository.CreateAsync(guestSubEvent);

            return mapper.Map<GuestSubEventResponse>(guestSubEvent);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GuestSubEventResponse>> GetByGuestIdAsync(int guestId)
        {
            throw new NotImplementedException();
        }

        public Task<GuestSubEventResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GuestSubEventResponse>> GetBySubEventIdAsync(int subEventId)
        {
            var subEvent = await guestSubEventRepository.GetAllAsync(e => e.SubEventId == subEventId, includeProperties: ["Guest"]) ?? throw new NotFoundException("GuestSubEvent");

            var response = mapper.Map<List<GuestSubEventResponse>>(subEvent);

            return response;
        }

        public Task<GuestSubEventResponse?> UpdateAsync(int id, SaveGuestSubEventRequest request)
        {
            throw new NotImplementedException();
        }
    }
}