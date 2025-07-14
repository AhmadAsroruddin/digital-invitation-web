using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Response
{
    public class GuestSubEventResponse
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public GuestResponse Guest { get; set; } = new GuestResponse();
        public int SubEventId { get; set; }
        public SubEventResponse SubEvent { get; set; } = new SubEventResponse();
    } 
}
