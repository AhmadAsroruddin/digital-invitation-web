using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Response
{
    public class SubEventStatisticResponse
    {
        public int EventId { get; set; }
        public EventResponse? Event { get; set; }
        public int SubEventId { get; set; }
        public SubEventResponse? SubEvent { get; set; }
        public int TotalInvitedGuest { get; set; }
        public int TotalRSVPGuest { get; set; }
        public int TotalAttendingRSVPGuest { get; set; }
        public int TotalNotAttendingRSVPGuest { get; set; }
        public int TotalCheckins { get; set; }
        public int TotalPeopleCheckin { get; set; }
        public int TotalGift { get; set; }
        public int TotalSouvenir { get; set; }
    }
}
namespace WebApi.Application.DTOs.Response
{
    public class EventStatisticResponse
    {
        public int EventId { get; set; }
        public EventResponse? Event { get; set; }
        public int TotalInvitedGuest { get; set; }
        public int TotalRSVPGuest { get; set; }
        public int TotalAttendingRSVPGuest { get; set; }
        public int TotalNotAttendingRSVPGuest { get; set; }
        public int TotalCheckins { get; set; }
        public int TotalPeopleCheckin { get; set; }
        public int TotalGift { get; set; }
        public int TotalSouvenir { get; set; }
    }
}
