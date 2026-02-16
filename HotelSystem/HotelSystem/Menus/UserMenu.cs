using HotelSystem.Models;
using HotelSystem.Services;

namespace HotelSystem.Menus
{
    internal class UserMenu
    {
        public static void Start(UserService users, RoomService rooms, BookingService bookings, ReviewService reviews, NotificationService notes)
        {
            while (true)
            {
                Console.WriteLine("=== User Menu ===");
                Console.WriteLine(users.GetLoginStatus());
                Console.WriteLine("1. Account Management");
                Console.WriteLine("2. Room Browsing");
                Console.WriteLine("3. Booking Management");
                Console.WriteLine("4. Reviews");
                Console.WriteLine("5. Notifications");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                try
                {
                    switch (k)
                    {
                        case "1": AccountManagement(users); break;
                        case "2": RoomBrowsing(rooms, bookings); break;
                        case "3": BookingManagement(bookings); break;
                        case "4": ReviewManagement(reviews); break;
                        case "5": NotificationManagement(notes); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }

        private static void AccountManagement(UserService users)
        {
            while (true)
            {
                Console.WriteLine("--- Account Management ---");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Update Profile");
                Console.WriteLine("4. Top Up Balance");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": users.Register(); break;
                    case "2": users.Login(); break;
                    case "3": users.UpdateProfile(); break;
                    case "4": users.TopUpBalance(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void RoomBrowsing(RoomService rooms, BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- Room Browsing ---");
                Console.WriteLine("1. View Rooms");
                Console.WriteLine("2. Search Rooms");
                Console.WriteLine("3. Check Availability");
                Console.WriteLine("4. Calculate Total Price");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": rooms.ViewRooms(); break;
                    case "2": rooms.SearchRooms(); break;
                    case "3": rooms.CheckAvailability(); break;
                    case "4": bookings.CalculateTotalPrice(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void BookingManagement(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- Booking Management ---");
                Console.WriteLine("1. Book Room");
                Console.WriteLine("2. View My Bookings");
                Console.WriteLine("3. Cancel Booking");
                Console.WriteLine("4. Extend Booking");
                Console.WriteLine("5. Change Room");
                Console.WriteLine("6. Late Checkout Request");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": bookings.BookRoom(); break;
                    case "2": bookings.ViewMyBookings(); break;
                    case "3": bookings.CancelBooking(); break;
                    case "4": bookings.ExtendBooking(); break;
                    case "5": bookings.ChangeRoom(); break;
                    case "6": bookings.LateCheckOutRequest(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void ReviewManagement(ReviewService reviews)
        {
            while (true)
            {
                Console.WriteLine("--- Reviews ---");
                Console.WriteLine("1. Rate Room");
                Console.WriteLine("2. View Room Reviews");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": reviews.RateRoom(); break;
                    case "2": reviews.ViewRoomReviews(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void NotificationManagement(NotificationService notes)
        {
            while (true)
            {
                Console.WriteLine("--- Notifications ---");
                Console.WriteLine("1. View Notifications");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": notes.ViewNotifications(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }
    }
}