using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
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
        [Route("event/{eventId}/guestlist-config")]
        public async Task<IActionResult> GetByEventId(int eventId)
        {
            var result = await guestlistConfigService.GetByEventIdAsync(eventId);

            return Success(result);
        }

        [HttpPut]
        [Route("guestlist-config/{guestlistConfigId}")]
        public async Task<IActionResult> UpdateAsync(int guestlistConfigId, [FromBody] SaveGuestlistConfigRequest request)
        {
            var result = await guestlistConfigService.UpdateAsync(guestlistConfigId, request);

            return Success(result);
        }

        [HttpGet]
        [Route("guestlist-config/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await guestlistConfigService.GetByIdAsync(id);

            return Success(result);
        }
    }
}