using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class MaintenanceService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Maintenance.json";
        private List<Maintenance> items = new();

        public void LoadMaintenance() => items = _fs.Load(path, new List<Maintenance>());
        private void Save() => _fs.Save(path, items);

        public void ScheduleMaintenance()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Date (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var date);
            Console.Write("Description: "); var desc = Console.ReadLine() ?? "";
            if (roomId <= 0 || date == default || string.IsNullOrWhiteSpace(desc))
                throw new Exception("Invalid maintenance data.");

            items.Add(new Maintenance { RoomId = roomId, Date = date, Description = desc, Status = "Scheduled" });
            Save();
            Console.WriteLine("Maintenance scheduled.");
        }

        public void ViewMaintenanceLog()
        {
            if (!items.Any()) { Console.WriteLine("No maintenance records."); return; }
            foreach (var m in items) Console.WriteLine($"#{m.Id} Room:{m.RoomId} {m.Date:yyyy-MM-dd} {m.Status} - {m.Description}");
        }
    }
}