namespace WebApi.Application.DTOs.Response
{
    public class RSVPResponse
    {
        public int Id { get; set; }
        public GuestSubEventResponse? GuestSubEvent;
        public string Status { get; set; } = "Not Attendance";
        public int PaxConfirmed { get; set; }
        public DateTime RSVPTime { get; set; }
    }

}
