using Domain.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Posts.Queries;

public record GetActivePostsQuery : IRequest<List<Post>>
{
    public string SocialNetworkName { get; set; }
}

public class GetActivePostsQueryHandler : IRequestHandler<GetActivePostsQuery, List<Post>>
{
    private readonly IAppDbContext _appDbContext;

    public GetActivePostsQueryHandler(IAppDbContext appDbContext) 
        => _appDbContext = appDbContext; 

    public async Task<List<Post>> Handle(GetActivePostsQuery request, CancellationToken cancellationToken)
    {
        return _appDbContext.Posts
            .Where(p => p.Expires >= DateTime.Now)
            .ToList();
    }
}
