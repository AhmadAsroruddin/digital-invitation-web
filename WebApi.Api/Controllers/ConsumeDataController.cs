using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class ConsumeDataController(IConsumeDataService consumeDataService) : BaseApiController
    {
        private readonly IConsumeDataService consumeDataService = consumeDataService;

        [HttpGet]
        [Route("pull-data/{eventId}")]
        public async Task<IActionResult> PullData(int eventId)
        {
            var result = await consumeDataService.PullData(eventId);

            return Success("asd");
        }
    }
}