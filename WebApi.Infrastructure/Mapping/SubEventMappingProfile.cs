using AutoMapper;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.SubEvent;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Mapping
{
    public class SubEventMappingProfile : Profile
    {
        public SubEventMappingProfile()
        {
            CreateMap<SaveSubEventRequest, SubEvent>();
            CreateMap<SubEvent, SubEventResponse>();
        }
    }
}