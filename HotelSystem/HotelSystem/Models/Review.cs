namespace HotelSystem.Models
{
    public class Review
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }
}