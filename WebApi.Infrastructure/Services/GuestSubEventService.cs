using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Shared;

namespace WebApi.Infrastructure.Services
{
    public class GuestSubEventService(IGuestSubEventRepository guestSubEventRepository, IMapper mapper, IHubContext<GuestListHub> hubContext) : IGuestSubEventService
    {
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<GuestListHub> _hubContext = hubContext;

        public async Task<GuestSubEventResponse> CreateAsync(int subEventId, SaveGuestSubEventRequest request)
        {
            var exists = await guestSubEventRepository.GetOneAsync(x => x.GuestId == request.GuestId && x.SubEventId == subEventId);

            if (exists != null)
            {
                throw new InvalidOperationException("Guest sudah terdaftar pada SubEvent ini.");
            }
            var guestSubEvent = mapper.Map<GuestSubEvent>(request);
            guestSubEvent.SubEventId = subEventId;

            await guestSubEventRepository.CreateAsync(guestSubEvent);

            var response = mapper.Map<GuestSubEventResponse>(guestSubEvent);

            await _hubContext.Clients.Group($"event_{guestSubEvent.SubEventId}")
                .SendAsync("EventEntityChanged", new
                {
                    type = "CHECKIN_UPDATED",
                    entity = response
                });
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