namespace WebApi.Application.DTOs.Response
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? GroomName { get; set; }
        public string? BrideName { get; set; }
        public string? GroomFamily { get; set; }
        public string? BrideFamily { get; set; }
        public List<SubEventResponse> SubEvents { get; set; } = [];
    }
}