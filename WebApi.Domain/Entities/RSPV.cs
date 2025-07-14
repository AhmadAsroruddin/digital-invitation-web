using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class RSVP : BaseEntity
    {
        public int Id { get; set; }
        public int GuestSubEventId { get; set; }
        [ForeignKey("GuestSubEventId")]
        public GuestSubEvent? GuestSubEvent { get; set; }
        public int SubEventId { get; set; }
        [ForeignKey("SubEventId")]
        public SubEvent? SubEvent { get; set; }
        public string Status { get; set; } = default!; 
        public int PaxConfirmed { get; set; }
        public DateTime RSVPTime { get; set; }
    }
}