namespace HotelSystem.Models
{
    public class Room
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public string Number { get; set; } = "";
        public string Type { get; set; } = ""; 
        public double Price { get; set; } = 0.0;
        public int Capacity { get; set; } = 1;
        public bool Available { get; set; } = true;
        public string Description { get; set; } = "";
        public List<string> Amenities { get; set; } = new();
        public List<DateTime> BanDates { get; set; } = new();
    }
}