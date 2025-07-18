using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;

public class GuestlistConfig : BaseEntity
{
    public int Id { get; set; }

    public int SubEventId { get; set; }  // Ganti dari EventId
    public string Name { get; set; } = default!;
    public string FilterJson { get; set; } = default!;
    public string ColumnsJson { get; set; } = default!;
    public string ShareCode { get; set; } = Guid.NewGuid().ToString("N");

    public new DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("SubEventId")]
    public SubEvent? SubEvent { get; set; }  // Ganti dari Event
}
