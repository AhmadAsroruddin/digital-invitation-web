using AutoMapper;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.SubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Exceptions;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class SubEventService(ISubEventRepository subEventRepository, IMapper mapper) : ISubEventService
    {
        private readonly ISubEventRepository subEventRepository = subEventRepository;
        private readonly IMapper mapper = mapper;

        public async Task<SubEventResponse> CreateAsync(int eventId,SaveSubEventRequest request)
        {
            var subEvent = mapper.Map<SubEvent>(request);
            subEvent.EventId = eventId;
            await subEventRepository.CreateAsync(subEvent);

            return mapper.Map<SubEventResponse>(subEvent);
        }


        public async Task<bool> DeletedAsync(int id, string userId)
        {
            var subEvent = await subEventRepository.GetOneAsync(e => e.Id == id, includeProperties: ["Event"]) ?? throw new NotFoundException("subEvent");

            if (subEvent.Event?.CreatedBy != userId)
            {
                throw new AccessForbiddenException("You don't have permission to delete this subEvent.");
            }

            await subEventRepository.DeleteAsync(subEvent);

            return true;
        }

        public async Task<IList<SubEventResponse>> GetAllAsync()
        {
            var subEvent = await subEventRepository.GetAllAsync();

            return mapper.Map<IList<SubEventResponse>>(subEvent);
        }

        public async Task<SubEventResponse> GetByIdAsync(int id, int eventId)
        {
            var subEvent = await subEventRepository.GetOneAsync(e => e.Id == id && e.EventId ==eventId, includeProperties: ["Event"]) ?? throw new NotFoundException("Sub Event");

            return mapper.Map<SubEventResponse>(subEvent);
        }

        public async Task<SubEventResponse> UpdateAsync(int subEventId, int eventId, string userId, SaveSubEventRequest request)
        {
            var subEvent = await subEventRepository.GetOneAsync(e => e.Id == subEventId && e.EventId ==eventId, includeProperties: ["Event"]) ?? throw new NotFoundException("subEvent");

            if (subEvent.Event?.CreatedBy != userId)
            {
                throw new AccessForbiddenException("You don't have permission to edit this contact.");
            }

            mapper.Map(request, subEvent);

            await subEventRepository.UpdateAsync(subEvent);

            return mapper.Map<SubEventResponse>(subEvent);
        }
    }
}