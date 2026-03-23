using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherPowerTest.Infrastructure.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с JSON
    /// </summary>
    public static class JsonHelper
    {
        public static bool TryParseJObject(string json, out JObject result)
        {
            try
            {
                result = JObject.Parse(json);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static T Deserialize<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }

        public static string SerializePretty(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}