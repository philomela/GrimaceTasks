using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.RestApi;
using Application.Posts.Commands;
using AutoMapper;
using Domain.Core;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Behaviors;

public class PostsRestApiPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IHttpClient<List<PostResponse>, string, string> _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PostsRestApiPreProcessor(IHttpClient<List<PostResponse>, string, string> httpClient, 
        IConfiguration configuration,
        IMapper mapper)
        => (_httpClient, _configuration, _mapper) = (httpClient, configuration, mapper);

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).GetCustomAttributes(typeof(PostsRestApiAttribute), true).Any())
        {
            var response = await _httpClient.GetAsync(_configuration["ExternalAddresses:Tasks"], cancellationToken);

            if (response == null || response.Count == 0)
            {
                throw new Exception("Response was null");
            }

            if (request is CreateOrUpdatePostsCommand createPostsCommand)
            {
                createPostsCommand.Posts = _mapper.Map<List<Post>>(response);
            }
        }

        return;
    }
}
