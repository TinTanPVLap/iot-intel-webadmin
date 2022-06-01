using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace adminiotintel.Helpers
{
    public class JsonConverterService
    {
        public string Serialize<TEntity>(TEntity obj) where TEntity : class
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public TEntity Deserialize<TEntity>(string str) where TEntity : class
        {
            return JsonConvert.DeserializeObject<TEntity>(str, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}