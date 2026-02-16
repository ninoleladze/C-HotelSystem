namespace HotelSystem.Models
{
    public class User
    {
        private static int _nextId = 1;
        public int Id { get; set; } = _nextId++;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Customer"; 
        public string Status { get; set; } = "Active"; 
        public double Balance { get; set; } = 0.0;
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}