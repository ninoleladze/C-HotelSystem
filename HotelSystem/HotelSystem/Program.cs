using HotelSystem.Menus;
using HotelSystem.Services;

namespace HotelSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var users = new UserService();
            var rooms = new RoomService();
            var discounts = new DiscountService();
            var notes = new NotificationService();
            var bookings = new BookingService(users, rooms, discounts, notes);
            var reviews = new ReviewService();
            var maintenance = new MaintenanceService();

            users.LoadUsers();
            rooms.LoadRooms();
            discounts.LoadDiscounts();
            bookings.LoadBookings();
            reviews.LoadReviews();
            notes.LoadNotifications();
            maintenance.LoadMaintenance();

            while (true)
            {
                Console.WriteLine("=== Hotel System ===");
                Console.WriteLine(users.GetLoginStatus()); 
                Console.WriteLine("1. User Menu");
                Console.WriteLine("2. Room Menu");
                Console.WriteLine("3. Booking Menu");
                Console.WriteLine("4. Manager Menu");
                Console.WriteLine("5. Admin Menu");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                var key = Console.ReadLine();

                switch (key)
                {
                    case "1": UserMenu.Start(users, rooms, bookings, reviews, notes); break;
                    case "2": RoomMenu.Start(rooms); break;
                    case "3": BookingMenu.Start(bookings); break;
                    case "4": ManagerMenu.Start(rooms, discounts, maintenance, notes); break;
                    case "5": AdminMenu.Start(users, rooms); break;
                    case "0": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }
    }
}