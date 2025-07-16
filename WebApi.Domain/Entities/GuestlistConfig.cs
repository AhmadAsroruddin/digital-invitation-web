using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class GuestlistConfig : BaseEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        public string Name { get; set; } = default!;
        public string FilterJson { get; set; } = default!;     
        public string ColumnsJson { get; set; } = default!;   
        public string ShareCode { get; set; } = Guid.NewGuid().ToString("N");

        public new DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey("EventId")]
        public Event? Event { get; set; }
    }

}