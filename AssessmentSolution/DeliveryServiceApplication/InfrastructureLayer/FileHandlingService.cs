using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DeliveryServiceApplication.InfrastructureLayer
{
    public class FileHandlingService
    {
        public static List<T> ReadFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(filePath)) ?? new List<T>();
        }

        public static void WriteFile<T>(string filePath, List<T> data)
        {
            string jsonToWrite = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonToWrite);
        }
    }
}
