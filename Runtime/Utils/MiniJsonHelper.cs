using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mini.Utils
{
    public static class MiniJsonHelper
    {
        public static T ReadFile<T>(string filename) where T : class
        {
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            return null;
        }

        public static T ReadFileOrDefault<T>(string filename) where T : new()
        {
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            return new T();
        }

        public static async Task WriteFileAsync<T>(string fullFileName, T jsonObject)
        {
            var directoryName = Path.GetDirectoryName(fullFileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }
            using (var file = File.CreateText(fullFileName))
            {
                using (var writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    await writer.WriteRawAsync(json);
                }
            }
        }
    }
}