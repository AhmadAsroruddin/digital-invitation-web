namespace WebApi.Application.DTOs.Request.Guest
{
    public class SaveGuestRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? InvitedBy { get; set; }
        public string? Notes { get; set; }
        public string? GuestGroup { get; set; }
    }
}
