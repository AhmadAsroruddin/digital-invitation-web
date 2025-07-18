namespace WebApi.Application.DTOs.Request.GuestListConfig
{
    public class SaveGuestlistConfigRequest
    {
        public int SubEventId { get; set; }
        public string Name { get; set; } = default!;
        public Dictionary<string, string> FilterJson { get; set; } = [];
        public List<string> ColumnsJson { get; set; } = [];
    }
}
