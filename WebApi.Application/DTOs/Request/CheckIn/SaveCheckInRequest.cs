namespace WebApi.Application.DTOs.Request.CheckIn
{
    public class SaveCheckInRequest
    {
        public int GuestSubEventId { get; set; }
        public int PaxActual { get; set; }
        public int GiftQty { get; set; }
        public int SouvenirQty { get; set; }
    }
}