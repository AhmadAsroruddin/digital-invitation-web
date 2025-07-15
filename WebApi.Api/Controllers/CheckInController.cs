using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.Interfaces.Service;
using WebApi.Infrastructure.Services;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/checkin/")]
    public class CheckInController(ICheckInService checkInService) : BaseApiController
    {
        private readonly ICheckInService checkInService = checkInService;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] SaveCheckInRequest request)
        {
            var result = await checkInService.CreateAsync(request);

            return Success(result);
        }

        [HttpGet]
        [Route("subevent/{subEventId}")]
        public async Task<IActionResult> GetAllBySubEventId(int subEventId)
        {
            var result = await checkInService.GetAllBySubEventAsync(subEventId);

            return Success(result);
        }

        [HttpGet]
        [Route("guestsubevent/{id}")]
        public async Task<IActionResult> GetCheckInByGuestSubEventId(int id)
        {
            var result = await checkInService.GetByIdAsync(id);

            return Success(result);
        }
    }
}