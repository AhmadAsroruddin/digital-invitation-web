using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class RSVPController(IRSVPService rSVPService) : BaseApiController
    {
        private readonly IRSVPService rSVPService = rSVPService;

        [HttpPost]
        [Route("guest-sub-event/{guestSubEventId}/rsvp")]
        public async Task<IActionResult> Create(int guestSubEventId, [FromBody] SaveRSVPRequest request)
        {
            var result = await rSVPService.CreateAsync(guestSubEventId, request);

            return Success(result);
        }
    }
}