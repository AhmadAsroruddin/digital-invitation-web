using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IGuestService
    {
        Task<GuestResponse> CreateAsync(int eventId,SaveGuestRequest request);
        Task<GuestResponse> GetByIdAsync(int id);
        Task<GuestResponse> UpdateAsync(int id,SaveGuestRequest request);
        Task<IList<GuestResponse>> GetAllAsync(int eventId);
        Task<bool> DeletedAsync(int guestId, int eventId);
    }
}