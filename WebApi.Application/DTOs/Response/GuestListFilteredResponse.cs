using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Response
{
    public class GuestlistFilteredResponse
    {
        public string ConfigurationName { get; set; } = default!;
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public List<GuestInList> Guests { get; set; } = [];
         public string? FilterJson { get; set; }
        public string? ColumnsJson { get; set; }
    }
}