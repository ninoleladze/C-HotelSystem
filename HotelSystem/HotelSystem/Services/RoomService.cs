using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class RoomService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Rooms.json";
        private List<Room> rooms = new();

        public void LoadRooms() => rooms = _fs.Load(path, new List<Room>());
        private void Save() => _fs.Save(path, rooms);

        public void ViewRooms()
        {
            if (!rooms.Any()) throw new Exception("No rooms available.");
            foreach (var r in rooms)
                Console.WriteLine($"#{r.Id} {r.Number} {r.Type} {r.Price:F2} Cap:{r.Capacity} {(r.Available ? "Available" : "Unavailable")}");
        }

        public void SearchRooms()
        {
            Console.Write("Search query: "); var q = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(q)) throw new Exception("Query required.");
            var found = rooms.Where(r =>
                r.Number.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                r.Type.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                r.Description.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!found.Any()) throw new Exception("No rooms found.");
            foreach (var r in found) Console.WriteLine($"#{r.Id} {r.Number} {r.Type} {r.Price:F2}");
        }

        public void CheckAvailability()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Start date (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("End date (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var end);
            var room = rooms.FirstOrDefault(r => r.Id == roomId) ?? throw new Exception("Room not found.");
            if (start == default || end == default || end <= start) throw new Exception("Invalid dates.");
            var unavailable = room.BanDates.Any(d => d >= start.Date && d <= end.Date);
            Console.WriteLine(room.Available && !unavailable ? "Available" : "Unavailable");
        }

        public void AddRoom()
        {
            Console.Write("Number: "); var num = Console.ReadLine() ?? "";
            Console.Write("Type: "); var type = Console.ReadLine() ?? "";
            Console.Write("Price: "); double.TryParse(Console.ReadLine(), out var price);
            Console.Write("Capacity: "); int.TryParse(Console.ReadLine(), out var cap);
            if (string.IsNullOrWhiteSpace(num) || string.IsNullOrWhiteSpace(type) || price <= 0 || cap <= 0)
                throw new Exception("Invalid room data.");
            var room = new Room { Number = num, Type = type, Price = price, Capacity = cap, Available = true };
            rooms.Add(room);
            Save();
            Console.WriteLine("Room added.");
        }

        public void RemoveRoom()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var id);
            var r = rooms.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Room not found.");
            rooms.Remove(r);
            Save();
            Console.WriteLine("Room removed.");
        }

        public void UpdatePrice()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var id);
            Console.Write("New price: "); double.TryParse(Console.ReadLine(), out var price);
            if (price <= 0) throw new Exception("Price must be positive.");
            var r = rooms.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Room not found.");
            r.Price = price;
            Save();
            Console.WriteLine("Price updated.");
        }

        public void UpdateRoomDetails()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var id);
            var r = rooms.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Room not found.");
            Console.Write("Description: "); r.Description = Console.ReadLine() ?? "";
            Console.Write("Amenities (comma separated): ");
            r.Amenities = (Console.ReadLine() ?? "").Split(',').Select(a => a.Trim()).Where(a => a.Length > 0).ToList();
            Save();
            Console.WriteLine("Details updated.");
        }

        public void BanDates()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var id);
            var r = rooms.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Room not found.");
            Console.Write("Ban start (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("Ban end (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var end);
            if (start == default || end == default || end < start) throw new Exception("Invalid ban dates.");
            for (var dt = start.Date; dt <= end.Date; dt = dt.AddDays(1)) r.BanDates.Add(dt);
            Save();
            Console.WriteLine("Dates banned.");
        }

        public Room? GetById(int id) => rooms.FirstOrDefault(r => r.Id == id);
    }
}