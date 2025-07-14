namespace WebApi.Application.DTOs.Response
{
    public class SubEventResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Location { get; set; }

    }
}