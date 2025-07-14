namespace WebApi.Application.DTOs.Request.SubEvent
{
    public class SaveSubEventRequest
    {
        public string? Name { get; set; }
        public int? EventId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
        public int MaxPax { get; set; } = 1;
    }

}