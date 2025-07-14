using WebApi.Application.Common;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IEventService
    {
        Task<EventResponse> CreateAsync(string userId, SaveEventRequest eventRequest);
        Task<EventResponse> GetByIdAsync(int id);
        Task<EventResponse> UpdateAsync(int id, string creatorId,SaveEventRequest request);
        Task<IList<EventResponse>> GetAllAsync();
        Task<bool> DeletedAsync(int id, string userId);
    }
}