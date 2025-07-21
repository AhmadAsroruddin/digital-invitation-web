using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Response
{
    public class GuestlistFilteredResponse
    {
        public string ConfigurationName { get; set; } = default!;
        public List<GuestInList> Guests { get; set; } = [];
        public string? FilterJson { get; set; }
        public string? ColumnsJson { get; set; }
        public SubEventResponse? SubEvent { get; set; }
        public EventResponse? Event { get; set; }
    }
}