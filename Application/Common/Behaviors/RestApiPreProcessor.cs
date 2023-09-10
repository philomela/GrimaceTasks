using Application.Common.Interfaces;
using Application.Common.RestApi;
using Application.Posts.Commands;
using Domain.Core;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Behaviors;

public class RestApiPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IHttpClient<List<Post>, string, string> _httpClient;
    private readonly IConfiguration _configuration;

    public RestApiPreProcessor(IHttpClient<List<Post>, string, string> httpClient, IConfiguration configuration) 
        => (_httpClient, _configuration) = (httpClient, configuration);
    
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).GetCustomAttributes(typeof(RestApiAttribute), true).Any())
        {
            var response = await _httpClient.GetAsync(_configuration["ExternalAddresses:Tasks"], cancellationToken);
            //await _appDbContext. Сохранить в бд запрос

            if (response == null || response.Count == 0)
            {
                throw new Exception("Response was null");
            }

            if (request is CreatePostsCommand createPostsCommand)
            {
                createPostsCommand.Posts = response;
            }

            return;

        }
        else
        {
            return;
        }
    }
}
