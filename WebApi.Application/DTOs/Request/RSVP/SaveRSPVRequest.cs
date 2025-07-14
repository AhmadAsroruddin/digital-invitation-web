namespace WebApi.Application.DTOs.Request.RSVP
{
    public class SaveRSVPRequest
    {
        public string Status { get; set; } = "Not Attendace";
        public int PaxConfirmed { get; set; }
    }

}