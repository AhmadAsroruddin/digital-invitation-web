using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities;

public class Guest :BaseEntity
{
    public int Id { get; set; }
    public int EventId { get; set; }
    [ForeignKey("EventId")]
    public Event? Event { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? GuestGroup { get; set; } 
    public int Pax { get; set; }
    public string? InvitedBy { get; set; } 
    public string? Notes { get; set; }

    public ICollection<RSVP>? RSVPs { get; set; }
    public ICollection<Checkin>? Checkins { get; set; }
}
