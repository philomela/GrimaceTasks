using Application.CheckResults.Commands;
using Application.Common.Interfaces;
using Application.Participants.Queries;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Hangfire;
using MediatR;

namespace Infrastructure.Hangfire;

public class ScheduledCheckPostsJob : IScheduledTasks
{
    private readonly IMediator _mediator;
    public ScheduledCheckPostsJob(IMediator mediator) 
        => _mediator = mediator;

    [AutomaticRetry(Attempts = 1)]
    public async Task ExecuteTaskOnSchedule()
    {
        var result = await _mediator.Send(new CreatePostsCommand());
        var posts = await _mediator.Send(new GetActivePostsQuery());
        var participants = await _mediator.Send(new GetParticipantsBySocialNetworkQuery());

        var checkResults = await _mediator.Send(new CheckPostsCommand() { ActivePosts = posts, Participants = participants });

        await _mediator.Send(new CreateCheckResultsCommand());
    }
}
