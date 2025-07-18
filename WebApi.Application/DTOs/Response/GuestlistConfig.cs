using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Response
{
    public class GuestlistConfigResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int SubEventId { get; set; }
        public int EventId { get; set; }
        public SubEventResponse? SubEvent { get; set; }
        public string? FilterJson { get; set; }
        public string? ColumnsJson { get; set; }
        public string ShareCode { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}