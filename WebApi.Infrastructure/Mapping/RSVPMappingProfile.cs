using AutoMapper;
using WebApi.Application.DTOs.Request.Contact;
using WebApi.Application.DTOs.Request.Event;
using WebApi.Application.DTOs.Request.Guest;
using WebApi.Application.DTOs.Request.RSVP;
using WebApi.Application.DTOs.Response;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Identity;

namespace WebApi.Infrastructure.Mapping
{
    public class RSVPMappingProfiile : Profile
    {
        public RSVPMappingProfiile()
        {
            CreateMap<SaveRSVPRequest, RSVP>();

            CreateMap<RSVP, RSVPResponse>();
        }
    }
}