using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IConsumeDataService
    {
       Task<bool> PullData(int eventId);
    }
}