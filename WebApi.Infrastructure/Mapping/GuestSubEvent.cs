using AutoMapper;
using WebApi.Application.DTOs.Request.GuestSubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Mapping
{
    public class GuestSubEventMappingProfile : Profile
    {
        public GuestSubEventMappingProfile()
        {
            CreateMap<SaveGuestSubEventRequest, GuestSubEvent>();

            CreateMap<GuestSubEvent, GuestSubEventResponse>();
        }
    }
}