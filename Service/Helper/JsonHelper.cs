using Newtonsoft.Json;

namespace Service
{
    public static class JsonHelper
    {
        //method to serialize string to json T
        public static T? Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonReaderException)
            {
                return default;
            }
        }

        //method to serialize object to json string
        public static string Serialize<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            } catch (JsonReaderException) {
                return "";
            }
        }
    }
}
