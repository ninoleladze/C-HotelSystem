namespace HotelSystem.Models
{
    public class Maintenance
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public int RoomId { get; set; }
        public string Description { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Scheduled";
    }
}