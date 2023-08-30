using Domain.Core;
using MediatR;

namespace Application.Posts.Commands;

public record CheckPostsCommand : IRequest<Unit>
{
    public List<Post> ActivePosts { get; set; }

    public List<Participant> Participants { get; set; }
}

public class CheckPostsCommandHandler : IRequestHandler<CheckPostsCommand, Unit>
{
    //Отсюда дергаем каждый сервис социальной сети, и запускаем проверку.
    //Результат пишем в таблицу проверненых, так как два раза на один пост юзер не может выполнить задание.???
    public Task<Unit> Handle(CheckPostsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
