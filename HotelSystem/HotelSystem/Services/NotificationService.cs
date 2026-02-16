using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class NotificationService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Notifications.json";
        private List<Notification> notes = new();

        public void LoadNotifications() => notes = _fs.Load(path, new List<Notification>());
        private void Save() => _fs.Save(path, notes);

        public void ViewNotifications()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            var list = notes.Where(n => n.UserId == UserService.CurrentUser!.Id || n.UserId == 0).ToList();
            if (!list.Any()) { Console.WriteLine("No notifications."); return; }
            foreach (var n in list) Console.WriteLine($"#{n.Id} [{n.Status}] {n.Date:yyyy-MM-dd} - {n.Message}");
        }

        public void AddNotification(int userId, string message)
        {
            var n = new Notification { UserId = userId, Message = message };
            notes.Add(n);
            Save();
        }
        public void SendNotification()
        {
            Console.Write("Target User Id (0 for all): "); int.TryParse(Console.ReadLine(), out var userId);
            Console.Write("Message: "); var msg = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(msg) || userId < 0) throw new Exception("Invalid notification data.");
            notes.Add(new Notification { UserId = userId, Message = msg });
            Save();
            Console.WriteLine("Notification sent.");
        }
    }
}