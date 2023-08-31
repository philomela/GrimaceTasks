using Domain.Core;
using Domain.Core.Dicitionarys;
using MediatR;
using static System.Net.WebRequestMethods;

namespace Application.Posts.Queries;

public record GetActivePostsQuery : IRequest<List<Post>>
{
}

public class GetActivePostsQueryHandler : IRequestHandler<GetActivePostsQuery, List<Post>>
{
    //Забираем все задачи которые активные из бд, и по которым нужно проверять логины на выполнимость. (Замокано)
    public async Task<List<Post>> Handle(GetActivePostsQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run<List<Post>>(() => new List<Post>
        {
            new Post
            {
                Id = 1,
                Points = 2,
                SocialNetwork = SocialNetworks.Instagram,
                Url = "https://www.instagram.com/p/Cvf9BoXMk18/"
            }
        });
    }
}
