using HotelSystem.Services;

namespace HotelSystem.Menus
{
    internal class AdminMenu
    {
        public static void Start(UserService users, RoomService rooms)
        {
            while (true)
            {
                Console.WriteLine("=== Admin Menu ===");
                Console.WriteLine("1. Block User");
                Console.WriteLine("2. Unblock User");
                Console.WriteLine("3. Ban Dates");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                try
                {
                    switch (k)
                    {
                        case "1": users.BlockUser(); break;
                        case "2": users.UnblockUser(); break;
                        case "3": rooms.BanDates(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }
    }
}