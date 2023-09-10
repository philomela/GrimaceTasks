using Application.Common.Interfaces;
using Application.Common.RestApi;
using Domain.Core;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Posts.Commands;

[RestApi]
public record CreatePostsCommand : IRequest<Unit>
{
    public List<Post> Posts { get; set; }
}

public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IHttpClient<List<Post>, string, string> _httpClient;
    private readonly IConfiguration _configuration;

    public CreatePostsCommandHandler(
        IAppDbContext appDbContext, 
        IHttpClient<List<Post>, string, string> httpClient, 
        IConfiguration configuration) 
        => (_appDbContext, _httpClient, _configuration) = (appDbContext, httpClient, configuration); 

    public async Task<Unit> Handle(CreatePostsCommand request, CancellationToken cancellationToken)
    {      
        await _appDbContext.Posts.AddRangeAsync(request.Posts, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
