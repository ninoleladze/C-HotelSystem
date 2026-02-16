namespace HotelSystem.Services
{
    public static class LoggerService
    {
        private static readonly object _lock = new();
        public static void Log(string message)
        {
            lock (_lock)
            {
                Console.WriteLine($"[LOG] {DateTime.UtcNow:o} - {message}");
            }
        }
    }
}
