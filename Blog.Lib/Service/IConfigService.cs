using LanguageExt.Common;
using Newtonsoft.Json.Linq;

namespace Blog.Lib.Service;

public interface IConfigService
{
    Task<Result<T>> GetJsonConfig<T>(string key);

    Task<Result<JObject>> GetJsonConfig(string key);
}
