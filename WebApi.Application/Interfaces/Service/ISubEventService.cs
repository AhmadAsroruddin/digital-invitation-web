using WebApi.Application.Common;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.SubEvent;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface ISubEventService
    {
        Task<SubEventResponse> CreateAsync(int eventId, SaveSubEventRequest request);
        Task<SubEventResponse> GetByIdAsync(int id, int eventId);
        Task<SubEventResponse> UpdateAsync(int subEventId, int eventId, string userId, SaveSubEventRequest request);
        Task<IList<SubEventResponse>> GetAllAsync();
        Task<bool> DeletedAsync(int id, string userId);
        Task<SubEventResponse> GetAllBySubEvent(int subEventId);
    }
}