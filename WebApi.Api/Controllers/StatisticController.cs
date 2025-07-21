using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.Interfaces.Service;
using WebApi.Infrastructure.Services;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class StatisticController(IStatisticService statisticService) : BaseApiController
    {
        private readonly IStatisticService statisticServic = statisticService;

        [HttpGet]
        [Route("sub-event/{subEventId}/statistic")]
        public async Task<IActionResult> GetSubEventStatistic(int subEventId)
        {
            var result = await statisticServic.GetSubEventStatistic(subEventId);

            return Success(result);
        }

        [HttpGet]
        [Route("event/{eventId}/statistic")]
        public async Task<IActionResult> GetEventStatistic(int eventId)
        {
            var result = await statisticServic.GetEventStatistic(eventId);

            return Success(result);
        }
    }
}