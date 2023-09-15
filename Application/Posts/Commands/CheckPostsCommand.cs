using Application.Common.Interfaces;
using Application.Posts.Queries;
using Domain.Core;
using MediatR;

namespace Application.Posts.Commands;

public record CheckPostsCommand : IRequest<Dictionary<long, List<Participant>>>
{
    public List<Post> Posts { get; set; }

    public List<Participant> Participants { get; set; }
}

public class CheckPostsCommandHandler : IRequestHandler<CheckPostsCommand, Dictionary<long, List<Participant>>>
{
    private readonly IInstagramService<Dictionary<long, List<Participant>>, Post, Participant> _instagramService;

    public CheckPostsCommandHandler(IInstagramService<Dictionary<long, List<Participant>>, Post, Participant> instagramService)
        => _instagramService = instagramService; 
    
    public async Task<Dictionary<long, List<Participant>>> Handle(CheckPostsCommand request, CancellationToken cancellationToken)
    {
       return await _instagramService.CheckPostsAsync(request.Posts, request.Participants);
    }
}
