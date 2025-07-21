using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;
using WebApi.Shared;

namespace WebApi.Infrastructure.Services
{
    public class GuestService(IGuestRepository guestRepository ,IGuestSubEventRepository guestSubEventRepository,IMapper mapper, IHubContext<GuestListHub> hubContext) : IGuestService
    {
        private readonly IGuestRepository guestRepository = guestRepository;
        private readonly IGuestSubEventRepository guestSubEventRepository = guestSubEventRepository;
        private readonly IMapper mapper = mapper;
        private readonly IHubContext<GuestListHub> _hubContext = hubContext;

        public async Task<GuestResponse> CreateAsync(int eventId, SaveGuestRequest request)
        {
            var guest = mapper.Map<Guest>(request);
            guest.EventId = eventId;
            await guestRepository.CreateAsync(guest);

            return mapper.Map<GuestResponse>(guest);
        }

        public async Task<bool> DeletedAsync(int guestId, int eventId)
        {
            var guest = await guestRepository.GetOneAsync(e => e.EventId == eventId && e.Id == guestId) ?? throw new NotFoundException("event");
        
            await guestRepository.DeleteAsync(guest);

            return true;
        }

        public async Task<IList<GuestResponse>> GetAllAsync(int eventId)
        {
            var guests = await guestRepository.GetAllAsync(e => e.EventId == eventId);
            var mappedGuest = mapper.Map<IList<GuestResponse>>(guests);
            return mappedGuest;
        }

        public async Task<GuestResponse> GetByIdAsync(int id)
        {
            var guest = await guestRepository.GetOneAsync(e => e.Id == id) ?? throw new NotFoundException("Guest");

            var response = mapper.Map<GuestResponse>(guest);

            return response;
        }

        public async Task<GuestResponse> UpdateAsync(int id, SaveGuestRequest request)
        {
            var guest = await guestRepository.GetOneAsync(e => e.Id == id) ?? throw new NotFoundException("Guest");

            mapper.Map(request, guest);

            await guestRepository.UpdateAsync(guest);

            var response = mapper.Map<GuestResponse>(guest);
            var subEventList = await guestSubEventRepository.GetAllAsync(e => e.GuestId == id);
            
            foreach (var subEvent in subEventList)
            {
                await _hubContext.Clients.Group($"event_{subEvent.SubEventId}")
                .SendAsync("EventEntityChanged", new
                {
                    type = "GUEST_UPDATED",
                    entity = response
                });
            }

            return response;
        }
    }
}