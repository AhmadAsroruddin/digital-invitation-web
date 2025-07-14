using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities;

public class GuestSubEvent : BaseEntity
{
    public int GuestSubEventId { get; set; }
    public int GuestId { get; set; }
    [ForeignKey("GuestId")]
    public Guest? Guest { get; set; }
    public int SubEventId { get; set; }
    [ForeignKey("SubEventId")]
    public SubEvent? SubEvent { get; set; }
    public int Pax { get; set; }

}