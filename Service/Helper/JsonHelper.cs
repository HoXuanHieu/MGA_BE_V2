using Newtonsoft.Json;

namespace Service
{
    public static class JsonHelper
    {
        //method to serialize string to json T
        public static T? Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        //method to serialize object to json string
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
