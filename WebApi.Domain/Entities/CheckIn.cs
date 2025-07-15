using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.Common;

namespace WebApi.Domain.Entities
{
    public class Checkin : BaseEntity
    {
        public int CheckinId { get; set; }
        public int GuestSubEventId { get; set; }
        [ForeignKey("GuestSubEventId")]
        public GuestSubEvent? GuestSubEvent { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckinTime { get; set; }
        public int PaxActual { get; set; }
        public int GiftQty { get; set; }
        public int SouvenirQty { get; set; }
    }

}