using HotelSystem.Services;

namespace HotelSystem.Menus
{
    internal class RoomMenu
    {
        public static void Start(RoomService rooms)
        {
            while (true)
            {
                Console.WriteLine("=== Room Menu ===");
                Console.WriteLine("1. View Rooms");
                Console.WriteLine("2. Search Rooms");
                Console.WriteLine("3. Check Availability");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                try
                {
                    switch (k)
                    {
                        case "1": rooms.ViewRooms(); break;
                        case "2": rooms.SearchRooms(); break;
                        case "3": rooms.CheckAvailability(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }
    }
}