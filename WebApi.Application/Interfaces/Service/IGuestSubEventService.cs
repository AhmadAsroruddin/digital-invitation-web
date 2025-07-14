using WebApi.Application.Common;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces.Service
{
    public interface IGuestSubEventService
    {
        Task<List<GuestSubEventResponse>> GetBySubEventIdAsync(int subEventId);
        Task<List<GuestSubEventResponse>> GetByGuestIdAsync(int guestId);
        Task<GuestSubEventResponse?> GetByIdAsync(int id);
        Task<GuestSubEventResponse> CreateAsync(int subEventId, SaveGuestSubEventRequest request);
        Task<GuestSubEventResponse?> UpdateAsync(int id, SaveGuestSubEventRequest request);
        Task<bool> DeleteAsync(int id);
    }
}