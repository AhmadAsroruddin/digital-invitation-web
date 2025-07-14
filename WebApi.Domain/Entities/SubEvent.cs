using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class SubEvent : BaseEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
        public string Name { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = default!;
        public ICollection<RSVP>? RSVPs { get; set; }
        public ICollection<Checkin>? Checkins { get; set; }
    }

}