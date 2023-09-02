using Application.Common.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class HttpClient : IHttpClient<string, string>
{
    private readonly IConfiguration _configuration;

    public HttpClient(IConfiguration configuration) 
        => _configuration = configuration;

    public async Task<string> GetAsync(CancellationToken token) 
        => await $"{_configuration["ExternalAddresses:ApiBot"]}".GetStringAsync(token);
        
    public async Task<string> PostAsync(string data, CancellationToken token) 
        => await $"{_configuration["ExternalAddresses:ApiBot"]}".PostStringAsync(data).ReceiveString();
}
