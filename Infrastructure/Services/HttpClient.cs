using Application.Common.Interfaces;
using Flurl.Http;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class HttpClient<T, K, P> : IHttpClient<T, K, P>
{
    public async Task<T> GetAsync(string url, CancellationToken token)
    {
        var result = await url.GetStringAsync(token);
        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task<P> PostAsync(string url, K data, CancellationToken token)
        => JsonConvert.DeserializeObject<P>(await url.PostJsonAsync(JsonConvert.SerializeObject(data)).ReceiveString());
}
