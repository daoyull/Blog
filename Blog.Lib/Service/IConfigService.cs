
using Newtonsoft.Json.Linq;

namespace Blog.Lib.Service;

public interface IConfigService
{
    Task<T> GetJsonConfig<T>(string key);

    Task<JObject> GetJsonConfig(string key);
}
