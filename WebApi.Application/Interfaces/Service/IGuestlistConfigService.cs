using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IGuestlistConfigService
    {
        Task<GuestlistConfigResponse> CreateAsync(SaveGuestlistConfigRequest request);
        Task<List<GuestlistConfigResponse>> GetByEventIdAsync(int eventId);
        Task<SaveGuestlistConfigRequest> GetByShareCodeAsync(string shareCode);
    }
}