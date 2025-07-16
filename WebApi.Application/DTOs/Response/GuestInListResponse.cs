using WebApi.Domain.Enums;

namespace WebApi.Application.DTOs.Response
{
    public class GuestInList
    {
        public int GuestId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? GuestGroup { get; set; }
        public string? InvitedBy { get; set; }
        public int Pax { get; set; }

        public List<SubEventResponse> SubEvents { get; set; } = new();
        public List<RSVPResponse> RSVPs { get; set; } = new();
    }
}