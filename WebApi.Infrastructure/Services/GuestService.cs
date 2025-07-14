using AutoMapper;
using Microsoft.Extensions.Logging;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class GuestService(IGuestRepository guestRepository, IMapper mapper) : IGuestService
    {
        private readonly IGuestRepository guestRepository = guestRepository;
        private readonly IMapper mapper = mapper;

        public async Task<GuestResponse> CreateAsync(int eventId,SaveGuestRequest request)
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

        public Task<GuestResponse> UpdateAsync(int id, SaveGuestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}