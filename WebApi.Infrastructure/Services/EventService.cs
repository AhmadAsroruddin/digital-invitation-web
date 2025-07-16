using AutoMapper;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class EventService(IEventRepository eventRepository, IMapper mapper) : IEventService
    {
        private readonly IEventRepository eventRepository = eventRepository;
        private readonly IMapper mapper = mapper;

        public async Task<EventResponse> CreateAsync(string userId, SaveEventRequest eventRequest)
        {
            var eventEntity = mapper.Map<Event>(eventRequest);
            eventEntity.CreatedBy = userId;
            await eventRepository.CreateAsync(eventEntity);

            return mapper.Map<EventResponse>(eventEntity);
        }

        public async Task<bool> DeletedAsync(int id, string userId)
        {
            var eventEntity = await eventRepository.GetByIdAsync(id) ?? throw new NotFoundException("event");

            if (eventEntity.CreatedBy != userId)
            {
                throw new AccessForbiddenException("You don't have permission to delete this event.");
            }

            await eventRepository.DeleteAsync(eventEntity);

            return true;
        }

        public async Task<IList<EventResponse>> GetAllAsync()
        {
            var events = await eventRepository.GetAllAsync();
            var allEvents = mapper.Map<IList<EventResponse>>(events);
            return allEvents;
        }

        public async Task<EventResponse> GetByIdAsync(int id)
        {
            var eventEntity = await eventRepository.GetOneAsync(e => e.Id == id, includeProperties: ["SubEvents"]) ?? throw new NotFoundException("Event");

            var response = mapper.Map<EventResponse>(eventEntity);

            return response;
        }

        public async Task<EventResponse> UpdateAsync(int id, string creatorId, SaveEventRequest request)
        {
            var eventEntity = await eventRepository.GetByIdAsync(id) ?? throw new NotFoundException("Event");

            mapper.Map(request, eventEntity);

            await eventRepository.UpdateAsync(eventEntity);

            return mapper.Map<EventResponse>(eventEntity);
        }
    }
}