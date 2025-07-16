using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/")]
    public class GuestlistConfigController(IGuestlistConfigService guestlistConfigService) : BaseApiController
    {
        private readonly IGuestlistConfigService guestlistConfigService = guestlistConfigService;

        [HttpPost]
        [Route("guestlist-config")]
        public async Task<IActionResult> Create([FromBody] SaveGuestlistConfigRequest request)
        {
            var result = await guestlistConfigService.CreateAsync(request);

            return Success(result);
        }

        [HttpGet]
        [Route("/event/{eventId}/guestlist-config")]
        public async Task<IActionResult> GetByEventId(int eventId)
        {
            var result = await guestlistConfigService.GetByEventIdAsync(eventId);

            return Success(result);
        }
    }
}