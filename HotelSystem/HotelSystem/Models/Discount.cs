namespace HotelSystem.Models
{
    public class Discount
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public int RoomId { get; set; }
        public double Percentage { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}