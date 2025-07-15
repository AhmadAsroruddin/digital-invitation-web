using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IRSVPService
    {
        Task<RSVPResponse> CreateAsync(int guestSubEventId,SaveRSVPRequest request);
        Task<RSVPResponse> GetByIdAsync(int id);
        Task<RSVPResponse> UpdateAsync(int id,SaveRSVPRequest request);
        Task<IList<RSVPResponse>> GetAllAsync(int eventId);
        Task<SubEventResponse> GetAllBySubEvent(int subEventId);
        Task<bool> DeletedAsync(int RSVPId, int eventId);
    }
}