using AutoMapper;
using WebApi.Application.DTOs.Request.Contact;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Identity;

namespace WebApi.Infrastructure.Mapping
{
    public class GuestMappingProfile : Profile
    {
        public GuestMappingProfile()
        {
            CreateMap<SaveGuestRequest, Guest>();

            CreateMap<Guest, GuestResponse>();
        }
    }
}