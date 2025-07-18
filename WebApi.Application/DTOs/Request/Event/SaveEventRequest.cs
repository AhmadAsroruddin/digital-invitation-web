using WebApi.Application.DTOs.Request.SubEvent;

namespace WebApi.Application.DTOs.Request.Event
{
    public class SaveEventRequest
    {
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? GroomName { get; set; }
        public string? BrideName { get; set; }
        public string? GroomFamily { get; set; }
        public string? BrideFamily { get; set; }
        public List<SaveSubEventRequest> SubEvents { get; set; } = [];
    }

}