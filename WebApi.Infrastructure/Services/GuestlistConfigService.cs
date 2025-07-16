using AutoMapper;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Services
{
    public class GuestlistConfigService(IGuestlistConfigRespository guestlistConfigRespository, IMapper mapper) : IGuestlistConfigService
    {
        private readonly IGuestlistConfigRespository guestlistConfigRespository = guestlistConfigRespository;
        private readonly IMapper mapper = mapper;

        public async Task<GuestlistConfigResponse> CreateAsync(SaveGuestlistConfigRequest request)
        {
            var guestlistConfig = mapper.Map<GuestlistConfig>(request);
            guestlistConfig.ShareCode = Guid.NewGuid().ToString("N");
            await guestlistConfigRespository.CreateAsync(guestlistConfig);

            return mapper.Map<GuestlistConfigResponse>(guestlistConfig);
        }

        public async Task<List<GuestlistConfigResponse>> GetByEventIdAsync(int eventId)
        {
            var guestlistConfig = await guestlistConfigRespository.GetAllAsync(e => e.EventId == eventId, includeProperties: ["Event"]);

            return mapper.Map<List<GuestlistConfigResponse>>(guestlistConfig);
        }

        public Task<SaveGuestlistConfigRequest> GetByShareCodeAsync(string shareCode)
        {
            throw new NotImplementedException();
        }
    }
}