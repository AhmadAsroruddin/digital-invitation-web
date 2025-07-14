using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
     [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/")]
    public class GuestSubEventController(IGuestSubEventService guestSubEventService) : BaseApiController
    {
        private readonly IGuestSubEventService guestSubEventService = guestSubEventService;

        [HttpPost]
        [Route("sub-events/{subEventId}/guests")]
        public async Task<IActionResult> Create(int subEventId, [FromForm] SaveGuestSubEventRequest request)
        {
            var result = await guestSubEventService.CreateAsync(subEventId, request);

            return Success(result, "Event Created");
        }
    }
}