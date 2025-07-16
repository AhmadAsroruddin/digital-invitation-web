using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class GuestController(IGuestService guestService) : BaseApiController
    {
        private readonly IGuestService guestService = guestService;

        [HttpPost]
        [Route("event/{eventId}/guest")]
        public async Task<IActionResult> Create(int eventId,[FromForm] SaveGuestRequest request)
        {
            var result = await guestService.CreateAsync(eventId,request);

            return Success(result, "Guest Created");
        }

        [HttpGet]
        [Route("event/{eventId}/guest/{guestId}")]
        public async Task<IActionResult> GetById(int guestId, int eventId)
        {
            var result = await guestService.GetByIdAsync(guestId);

            return Success(result);
        }

        [HttpGet]
        [Route("event/{eventId}/guest")]
        public async Task<IActionResult> GetAll(int eventId)
        {
            var result = await guestService.GetAllAsync(eventId);

            return Success(result);
        }

        [HttpDelete]
        [Route("event/{eventId}/guest/{guestId}")]
        public async Task<IActionResult> DeleteById(int guestId, int eventId)
        {
            await guestService.DeletedAsync(guestId, eventId);

            return Success<object>(null, "Event Deleted");
        }

    }
}