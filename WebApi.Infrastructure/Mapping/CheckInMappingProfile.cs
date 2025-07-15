using AutoMapper;
using WebApi.Application.DTOs.Request.CheckIn;
using WebApi.Application.DTOs.Request.Contact;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Identity;

namespace WebApi.Infrastructure.Mapping
{
    public class CheckInMappingProfile : Profile
    {
        public CheckInMappingProfile()
        {
            CreateMap<SaveCheckInRequest, Checkin>();

            CreateMap<Checkin, CheckInResponse>();
        }
    }
}