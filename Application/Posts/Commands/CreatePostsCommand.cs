using MediatR;
using System.Net;

namespace Application.Posts.Commands;

public record CreatePostsCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public int NamePost { get; set; }
}

public class CreatePostsCommandHandler : IRequestHandler<CreatePostsCommand, Unit>
{
    //Идем в апи бота, забираем посты, раскладываем в бд данные по постам.
    public Task<Unit> Handle(CreatePostsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
