using HotelSystem.Models;
using System.Text.Json;

namespace HotelSystem.Services
{
    internal class BookingService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Bookings.json";
        private List<Booking> bookings = new();

        private readonly UserService _users;
        private readonly RoomService _rooms;
        private readonly DiscountService _discounts;
        private readonly NotificationService _notes;

        public BookingService(UserService users, RoomService rooms, DiscountService discounts, NotificationService notes)
        {
            _users = users;
            _rooms = rooms;
            _discounts = discounts;
            _notes = notes;
        }

        public void LoadBookings() => bookings = _fs.Load(path, new List<Booking>());
        private void Save() => _fs.Save(path, bookings);

        public void CalculateTotalPrice()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Start (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("End (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var end);

            var room = _rooms.GetById(roomId) ?? throw new Exception("Room not found.");
            if (start == default || end == default || end <= start) throw new Exception("Invalid dates.");

            var nights = (end.Date - start.Date).Days;
            var basePrice = room.Price * nights;
            var total = _discounts.ApplyDiscount(roomId, start, end, basePrice);
            total = ApplyDynamicPricing(total, start, end);

            Console.WriteLine($"Total price: {total:F2} for {nights} nights.");
        }

        public void BookRoom()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");

            Console.WriteLine("=== Available Rooms ===");
            _rooms.ViewRooms();

            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Start (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("End (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var end);

            var room = _rooms.GetById(roomId) ?? throw new Exception("Room not found.");
            if (!room.Available) throw new Exception("Room unavailable.");
            if (start == default || end == default || end <= start) throw new Exception("Invalid dates.");
            if (room.BanDates.Any(d => d >= start.Date && d <= end.Date))
                throw new Exception("Room is banned for selected dates.");

            var nights = (end.Date - start.Date).Days;
            var total = room.Price * nights;

            if (UserService.CurrentUser!.Balance < total) throw new Exception("Insufficient balance.");

            Console.Write("Special requests (optional): "); var req = Console.ReadLine() ?? "";

            UserService.CurrentUser!.Balance -= total;
            var booking = new Booking
            {
                UserId = UserService.CurrentUser!.Id,
                RoomId = roomId,
                StartDate = start,
                EndDate = end,
                TotalPrice = total,
                SpecialRequests = req
            };
            bookings.Add(booking);
            Save();

            Console.WriteLine($"Booked successfully. Booking #{booking.Id}, total {total:F2}");

            _notes.AddNotification(UserService.CurrentUser!.Id,
                $"Your booking #{booking.Id} for Room {room.Number} is confirmed from {start:yyyy-MM-dd} to {end:yyyy-MM-dd}.");
        }

        public void ViewMyBookings()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            var list = bookings.Where(b => b.UserId == UserService.CurrentUser!.Id).ToList();
            if (!list.Any()) { Console.WriteLine("No bookings."); return; }
            foreach (var b in list) Console.WriteLine(b); // ToString override
        }

        public void CancelBooking()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            Console.Write("Booking Id: "); int.TryParse(Console.ReadLine(), out var id);
            var b = bookings.FirstOrDefault(x => x.Id == id && x.UserId == UserService.CurrentUser!.Id && x.Status == "Active")
                    ?? throw new Exception("Booking not found.");
            b.Status = "Cancelled";
            UserService.CurrentUser!.Balance += b.TotalPrice;
            Save();
            Console.WriteLine("Booking cancelled and refunded.");
        }

        public void ExtendBooking()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            Console.Write("Booking Id: "); int.TryParse(Console.ReadLine(), out var id);
            Console.Write("Extend days: "); int.TryParse(Console.ReadLine(), out var days);
            var b = bookings.FirstOrDefault(x => x.Id == id && x.UserId == UserService.CurrentUser!.Id && x.Status == "Active")
                    ?? throw new Exception("Booking not found.");

            var newEnd = b.EndDate.AddDays(days > 0 ? days : 0);
            if (days <= 0) throw new Exception("Days must be positive.");
            var room = _rooms.GetById(b.RoomId) ?? throw new Exception("Room not found.");
            var conflict = room.BanDates.Any(d => d > b.EndDate && d <= newEnd);
            if (conflict) throw new Exception("Extension conflicts with ban dates.");

            var extra = ApplyDynamicPricing(room.Price * days, b.EndDate, newEnd);
            if (UserService.CurrentUser!.Balance < extra) throw new Exception("Insufficient balance.");
            UserService.CurrentUser!.Balance -= extra;
            b.EndDate = newEnd; b.TotalPrice += extra;
            Save();
            Console.WriteLine($"Extended to {newEnd:yyyy-MM-dd}. Extra {extra:F2}");
        }

        public void ChangeRoom()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            Console.Write("Booking Id: "); int.TryParse(Console.ReadLine(), out var id);
            var b = bookings.FirstOrDefault(x => x.Id == id && x.UserId == UserService.CurrentUser!.Id && x.Status == "Active")
                    ?? throw new Exception("Booking not found.");
            Console.Write("New Room Id: "); int.TryParse(Console.ReadLine(), out var newRoomId);
            var newRoom = _rooms.GetById(newRoomId) ?? throw new Exception("Room not found.");
            if (!newRoom.Available) throw new Exception("New room unavailable.");

            b.RoomId = newRoomId;
            Save();
            Console.WriteLine("Room changed.");
        }

        public void ViewAllBookings()
        {
            if (!bookings.Any()) { Console.WriteLine("No bookings found."); return; }
            foreach (var b in bookings) Console.WriteLine(b);
        }

        public void ExportBookings()
        {
            var exportPath = "Bookings.export.json";
            try
            {
                var json = JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(exportPath, json);
                Console.WriteLine($"Bookings exported to {exportPath}");
            }
            catch { Console.WriteLine("Error: Could not export bookings."); }
        }

        public void LateCheckOutRequest()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            Console.Write("Booking Id: "); int.TryParse(Console.ReadLine(), out var id);
            var b = bookings.FirstOrDefault(x => x.Id == id && x.UserId == UserService.CurrentUser!.Id && x.Status == "Active")
                    ?? throw new Exception("Booking not found.");
            b.LateCheckoutRequested = true;
            Save();
            Console.WriteLine("Late checkout requested.");
        }

        private double ApplyDynamicPricing(double amount, DateTime start, DateTime end)
        {
            var nights = (end.Date - start.Date).Days;
            if (nights <= 0) return amount;
            double factor = 1.0;
            for (var dt = start.Date; dt < end.Date; dt = dt.AddDays(1))
            {
                if (dt.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday) factor += 0.10 / nights;
                if (dt.Month is 7 or 8) factor += 0.15 / nights;
            }
            return Math.Round(amount * factor, 2);
        }
    }
}