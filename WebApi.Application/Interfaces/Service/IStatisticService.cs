using WebApi.Application.DTOs.Response;

namespace WebApi.Application.Interfaces.Service
{
    public interface IStatisticService
    {
        public Task<SubEventStatisticResponse> GetSubEventStatistic(int subEventId);
        public Task<EventStatisticResponse> GetEventStatistic(int eventId);
    }
}