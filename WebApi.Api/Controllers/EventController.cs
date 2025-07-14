using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/event")]
    public class EventController (IEventService eventService) : BaseApiController
    {
        private readonly IEventService eventService = eventService;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] SaveEventRequest request)
        {
            var userId = GetLoggedInUserId()!;

            var result = await eventService.CreateAsync(userId, request);

            return Success(result, "Event Created");
        }
    }
}