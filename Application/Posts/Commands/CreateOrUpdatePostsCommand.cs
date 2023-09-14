using Application.Common.RestApi;
using Domain.Core;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Posts.Commands;

[PostsRestApi]
public record CreateOrUpdatePostsCommand : IRequest<Unit>
{
    public List<Post> Posts { get; set; }
}

public class CreateOrUpdatePostsCommandHandler : IRequestHandler<CreateOrUpdatePostsCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IConfiguration _configuration;

    public CreateOrUpdatePostsCommandHandler(
        IAppDbContext appDbContext, 
        IConfiguration configuration) 
        => (_appDbContext, _configuration) = (appDbContext, configuration); 

    public async Task<Unit> Handle(CreateOrUpdatePostsCommand request, CancellationToken cancellationToken)
    {
        var postIds = request.Posts
            .Select(p => p.Id)
            .ToList();

        var existingPosts = _appDbContext.Posts
            .Where(p => postIds.Contains(p.Id))
            .ToList();

        var postsToAdd = request.Posts
            .Where(p => !existingPosts
            .Contains(p))
            .ToList();

        await _appDbContext.Posts
            .AddRangeAsync(postsToAdd, cancellationToken);

        var postsToUpdate = existingPosts
            .Where(p => request.Posts
            .Any(rp => rp.UpdatedAt > p.UpdatedAt))
            .ToList();

        _appDbContext.Posts.UpdateRange(postsToUpdate);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
