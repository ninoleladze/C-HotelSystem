namespace HotelSystem.Models
{
    public class Booking
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } = "Active";
        public string SpecialRequests { get; set; } = "";
        public bool LateCheckoutRequested { get; set; } = false;

        public override string ToString()
        {
            var nights = Math.Max(0, (EndDate.Date - StartDate.Date).Days);
            var late = LateCheckoutRequested ? "LateCO:Yes" : "LateCO:No";
            var req = string.IsNullOrWhiteSpace(SpecialRequests) ? "" : $" | Req:{SpecialRequests}";
            return $"#{Id} User:{UserId} Room:{RoomId} | {StartDate:yyyy-MM-dd}..{EndDate:yyyy-MM-dd} ({nights}n) | {Status} | {late} | Total:{TotalPrice:F2}{req}";
        }
    }
}