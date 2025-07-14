namespace WebApi.Application.DTOs.Request.Event
{
    public class SaveSubEventRequest
    {
        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }
    }

}