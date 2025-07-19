using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Interfaces.Service;

namespace WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/guest-list/")]
    public class GuestlistController(IGuestlistConfigService guestlistConfigService) : BaseApiController
    {
        [HttpGet]
        [Route("{shareCodeID}")]
        public async Task<IActionResult> GetFilteredGuestlist(string shareCodeId)
        {
            var result = await guestlistConfigService.GetByShareCodeAsync(shareCodeId);
            
            return Success(result);
        }
    }
}
