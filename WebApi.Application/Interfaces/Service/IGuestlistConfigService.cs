using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IGuestlistConfigService
    {
        Task<GuestlistConfigResponse> CreateAsync(SaveGuestlistConfigRequest request);
        Task<List<GuestlistConfigResponse>> GetByEventIdAsync(int eventId);
        Task<GuestlistFilteredResponse> GetByShareCodeAsync(string shareCode);
        Task<GuestlistConfigResponse> UpdateAsync(int gueslistConfigId, SaveGuestlistConfigRequest request);
        Task<GuestlistConfigResponse> GetByIdAsync(int Id);
        Task<bool> DeleteById(int id);
    }
}