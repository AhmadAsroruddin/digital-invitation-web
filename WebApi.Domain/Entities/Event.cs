using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class Event : BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? GroomName { get; set; }
        public string? BrideName { get; set; }
        public string? GroomFamily { get; set; }
        public string? BrideFamily { get; set; }
        public ICollection<SubEvent>? SubEvents {get;set;}
    }
}