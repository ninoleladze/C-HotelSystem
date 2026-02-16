namespace HotelSystem.Models
{
    public class Notification
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public int UserId { get; set; }
        public string Message { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Unread";
    }
}