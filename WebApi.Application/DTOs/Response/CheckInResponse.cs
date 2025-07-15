namespace WebApi.Application.DTOs.Response
{
    public class CheckInResponse
    {
        public int CheckinId { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime CheckinTime { get; set; }
        public int PaxActual { get; set; }
        public int GiftQty { get; set; }
        public int SouvenirQty { get; set; }
    }
}