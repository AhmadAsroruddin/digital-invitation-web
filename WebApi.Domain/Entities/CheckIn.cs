using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class Checkin : BaseEntity
    {
        public int CheckinId { get; set; }
        public int GuestId { get; set; }
        [ForeignKey("GeustId")]
        public Guest? Guest { get; set; }
        public int SubEventId { get; set; }
        [ForeignKey("SubEventId")]
        public SubEvent? SubEvent { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckinTime { get; set; }
        public int PaxActual { get; set; }
        public int GiftQty { get; set; }
        public int SouvenirQty { get; set; }
    }

}