using HotelSystem.Services;

namespace HotelSystem.Menus
{
    internal class ManagerMenu
    {
        public static void Start(RoomService rooms, DiscountService discounts, MaintenanceService maintenance, NotificationService notes)
        {
            while (true)
            {
                Console.WriteLine("=== Manager Menu ===");
                Console.WriteLine("1. Room Management");
                Console.WriteLine("2. Discount Management");
                Console.WriteLine("3. Maintenance Management");
                Console.WriteLine("4. Notifications");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                try
                {
                    switch (k)
                    {
                        case "1": RoomManagement(rooms); break;
                        case "2": DiscountManagement(discounts); break;
                        case "3": MaintenanceManagement(maintenance); break;
                        case "4": NotificationManagement(notes); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            }
        }

        private static void RoomManagement(RoomService rooms)
        {
            while (true)
            {
                Console.WriteLine("--- Room Management ---");
                Console.WriteLine("1. Add Room");
                Console.WriteLine("2. Remove Room");
                Console.WriteLine("3. Update Price");
                Console.WriteLine("4. Update Room Details");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": rooms.AddRoom(); break;
                    case "2": rooms.RemoveRoom(); break;
                    case "3": rooms.UpdatePrice(); break;
                    case "4": rooms.UpdateRoomDetails(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void DiscountManagement(DiscountService discounts)
        {
            while (true)
            {
                Console.WriteLine("--- Discount Management ---");
                Console.WriteLine("1. Manage Discounts");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": discounts.ManageDiscounts(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }

        private static void MaintenanceManagement(MaintenanceService maintenance)
        {
            while (true)
            {
                Console.WriteLine("--- Maintenance Management ---");
                Console.WriteLine("1. Schedule Maintenance");
                Console.WriteLine("2. View Maintenance Log");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": maintenance.ScheduleMaintenance(); break;
                    case "2": maintenance.ViewMaintenanceLog(); break;
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
                Console.WriteLine("1. Send Notification");
                Console.WriteLine("0. Back");
                var k = Console.ReadLine();

                switch (k)
                {
                    case "1": notes.SendNotification(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid"); break;
                }
            }
        }
    }
}