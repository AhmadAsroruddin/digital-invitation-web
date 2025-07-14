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
    public class EventController(IEventService eventService) : BaseApiController
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await eventService.GetByIdAsync(id);

            return Success(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await eventService.GetAllAsync();

            return Success(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var userId = GetLoggedInUserId()!;

            await eventService.DeletedAsync(id, userId);

            return Success<object>(null, "Event Deleted");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] SaveEventRequest request)
        {
            var userId = GetLoggedInUserId()!;

            var response = await eventService.UpdateAsync(id, userId, request);

            return Success(response, "Update Success");
        }
    }
}