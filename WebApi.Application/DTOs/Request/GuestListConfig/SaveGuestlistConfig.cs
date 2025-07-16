namespace WebApi.Application.DTOs.Request.GuestListConfig
{
    public class SaveGuestlistConfigRequest
    {
        public int EventId { get; set; }
        public string Name { get; set; } = default!;
        public Dictionary<string, string> Filter { get; set; } = [];
        public List<string> Columns { get; set; } = [];
    }
}
