using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
     public interface ICheckInService
    {
        Task<CheckInResponse> CreateAsync(SaveCheckInRequest request);
        Task<CheckInResponse> GetByIdAsync(int id);
        Task<IList<CheckInResponse>> GetAllBySubEventAsync(int subEventId);
    }
}