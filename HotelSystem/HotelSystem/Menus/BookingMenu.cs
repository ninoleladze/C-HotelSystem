using HotelSystem.Services;

namespace HotelSystem.Menus
{
    internal class BookingMenu
    {
        public static void Start(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("=== Booking Menu ===");
                Console.WriteLine("1. Make a Booking");
                Console.WriteLine("2. My Bookings");
                Console.WriteLine("3. Manage Bookings");
                Console.WriteLine("4. Reports");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                try
                {
                    switch (k)
                    {
                        case "1": MakeBooking(bookings); break;
                        case "2": MyBookings(bookings); break;
                        case "3": ManageBookings(bookings); break;
                        case "4": Reports(bookings); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }

        private static void MakeBooking(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- Make a Booking ---");
                Console.WriteLine("1. Calculate Total Price");
                Console.WriteLine("2. Book Room");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": bookings.CalculateTotalPrice(); break;
                    case "2": bookings.BookRoom(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void MyBookings(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- My Bookings ---");
                Console.WriteLine("1. View My Bookings");
                Console.WriteLine("2. Cancel Booking");
                Console.WriteLine("3. Extend Booking");
                Console.WriteLine("4. Change Room");
                Console.WriteLine("5. Late Checkout Request");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": bookings.ViewMyBookings(); break;
                    case "2": bookings.CancelBooking(); break;
                    case "3": bookings.ExtendBooking(); break;
                    case "4": bookings.ChangeRoom(); break;
                    case "5": bookings.LateCheckOutRequest(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void ManageBookings(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- Manage Bookings (Manager/Admin) ---");
                Console.WriteLine("1. View All Bookings");
                Console.WriteLine("2. Export Bookings");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": bookings.ViewAllBookings(); break;
                    case "2": bookings.ExportBookings(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void Reports(BookingService bookings)
        {
            while (true)
            {
                Console.WriteLine("--- Reports ---");
                Console.WriteLine("1. Booking Statistics (future extension)");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": Console.WriteLine("Statistics not yet implemented."); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }
    }
}