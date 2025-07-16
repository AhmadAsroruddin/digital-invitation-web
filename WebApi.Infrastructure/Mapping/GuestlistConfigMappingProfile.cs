using AutoMapper;
using WebApi.Application.DTOs.Request.GuestListConfig;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Mapping
{
    public class GuestlistConfigMappingProfile : Profile
    {
        public GuestlistConfigMappingProfile()
        {
            CreateMap<SaveGuestlistConfigRequest, GuestlistConfig>();

            CreateMap<GuestlistConfig, GuestlistConfigResponse>();
        }
    }
}