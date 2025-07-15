namespace WebApi.Application.DTOs.Request.RSVP
{
    public class SaveRSVPRequest
    {
        public string Status { get; set; } = "Not Attendance";
        public int PaxConfirmed { get; set; }
    }

}