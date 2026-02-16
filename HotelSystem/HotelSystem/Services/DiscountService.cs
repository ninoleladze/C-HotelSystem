using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class DiscountService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Discounts.json";
        private List<Discount> discounts = new();

        public void LoadDiscounts() => discounts = _fs.Load(path, new List<Discount>());
        private void Save() => _fs.Save(path, discounts);

        public void ManageDiscounts()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Percentage (0-100): "); double.TryParse(Console.ReadLine(), out var pct);
            Console.Write("Start (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("End (yyyy-MM-dd): "); DateTime.TryParse(Console.ReadLine(), out var end);
            if (roomId <= 0 || pct < 0 || pct > 100 || start == default || end == default || end < start)
                throw new Exception("Invalid discount data.");
            discounts.Add(new Discount { RoomId = roomId, Percentage = pct, StartDate = start, EndDate = end });
            Save();
            Console.WriteLine("Discount added.");
        }

        public double ApplyDiscount(int roomId, DateTime start, DateTime end, double amount)
        {
            var active = discounts.FirstOrDefault(d =>
                d.RoomId == roomId &&
                (start <= d.EndDate && end >= d.StartDate));
            if (active == null) return amount;
            return Math.Round(amount * (1 - active.Percentage / 100.0), 2);
        }
    }
}