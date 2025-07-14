namespace WebApi.Application.DTOs.Response
{
    public class SubEventResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public int MaxPax { get; set; }
        public int EventId { get; set; }
    }   
}