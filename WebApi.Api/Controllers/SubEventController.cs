using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.SubEvent;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/")]
    public class SubEventController(ISubEventService subEventService) : BaseApiController
    {
        private readonly ISubEventService subEventService = subEventService;

        [HttpPost]
        [Route("event/{eventId}/sub-event")]
        public async Task<IActionResult> Create(int eventId, [FromForm] SaveSubEventRequest request)
        {
            var result = await subEventService.CreateAsync(eventId, request);

            return Success(result, "Sub Event Created");
        }

        [HttpGet]
        [Route("event/{eventId}/sub-event/{subEventId}")]
        public async Task<IActionResult> GetById(int eventId, int subEventId)
        {
            var result = await subEventService.GetByIdAsync(subEventId, eventId);

            return Success(result);
        }

        [HttpGet]
        [Route("event/{eventId}/sub-event")]
        public async Task<IActionResult> GetAll(int eventId)
        {
            var result = await subEventService.GetAllAsync();

            return Success(result);
        }

        [HttpDelete]
        [Route("event/{eventId}/sub-event/{subEventId}")]
        public async Task<IActionResult> DeleteById(int eventId, int subEventId)
        {
            var userId = GetLoggedInUserId()!;

            await subEventService.DeletedAsync(subEventId, userId);

            return Success<object>(null, "Event Deleted");
        }

        [HttpPut]
        [Route("event/{eventId}/sub-event/{subEventId}")]
        public async Task<IActionResult> Update(int eventId, int subEventId, [FromForm] SaveSubEventRequest request)
        {
            var userId = GetLoggedInUserId()!;

            var response = await subEventService.UpdateAsync(subEventId, eventId, userId, request);
            return Success(response, "Update Success");
        }
        
        [HttpGet]
        [Route("sub-event/{subEventId}/rsvp")]
        public async Task<IActionResult> GetAllRSVPByESubEvent(int subEventId)
        {
            var result = await subEventService.GetAllBySubEvent(subEventId);

            return Success(result);
        }
    }
}