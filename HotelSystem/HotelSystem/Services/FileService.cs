using System.Text.Json;

namespace HotelSystem.Services
{
    internal class FileService
    {
        public T Load<T>(string path, T defaultValue)
        {
            try
            {
                if (!File.Exists(path)) return defaultValue;
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<T>(json);
                return data is null ? defaultValue : data;
            }
            catch { return defaultValue; }
        }

        public void Save<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}