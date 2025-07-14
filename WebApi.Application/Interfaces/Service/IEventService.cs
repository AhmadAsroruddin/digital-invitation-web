using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IEventService
    {
        Task<EventResponse> CreateAsync(string userId, SaveEventRequest eventRequest);
    }
}