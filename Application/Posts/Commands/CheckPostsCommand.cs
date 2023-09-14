using Application.Common.Interfaces;
using Application.Posts.Queries;
using Domain.Core;
using MediatR;

namespace Application.Posts.Commands;

public record CheckPostsCommand : IRequest<Dictionary<string, List<string>>>
{
    public List<Post> Posts { get; set; }

    public List<Participant> Participants { get; set; }
}

public class CheckPostsCommandHandler : IRequestHandler<CheckPostsCommand, Dictionary<string, List<string>>>
{
    private readonly IInstagramService<Dictionary<string, List<string>>, Post, Participant> _instagramService;

    public CheckPostsCommandHandler(IInstagramService<Dictionary<string, List<string>>, Post, Participant> instagramService)
        => _instagramService = instagramService; 
    //Отсюда дергаем каждый сервис социальной сети, и запускаем проверку.
    //Результат пишем в таблицу проверненых, так как два раза на один пост юзер не может выполнить задание.???
    public async Task<Dictionary<string, List<string>>> Handle(CheckPostsCommand request, CancellationToken cancellationToken)
    {
       return await _instagramService.CheckPostAsync(request.Posts.FirstOrDefault(), request.Participants);
    }
}
