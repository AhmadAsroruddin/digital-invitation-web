namespace WebApi.Application.DTOs.Response
{
    public class RSVPResponse
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int SubEventId { get; set; }
        public string Status { get; set; } = "Not Attendance";
        public int PaxConfirmed { get; set; }
        public DateTime RSVPTime { get; set; }
    }

}
