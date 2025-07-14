using AutoMapper;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Response;
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
            var events = mapper.Map<Event>(eventRequest);
            events.CreatedBy = userId;
            await eventRepository.CreateAsync(events);

            return mapper.Map<EventResponse>(events);
        }
    }
}