using Application.CheckResults.Commands;
using Application.Common.Interfaces;
using Application.Participants.Commands;
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
        await _mediator.Send(new CreateOrUpdatePostsCommand());
        await _mediator.Send(new CreateParticipantsCommand());

        var instaPosts = await _mediator.Send(new GetActivePostsQuery()); //Выбрать нужных по переданному socialNetworkName
        var instaParticipants = await _mediator.Send(new GetParticipantsBySocialNetworkQuery()); //Выбрать нужных по переданному socialNetworkName

        var checkResults = await _mediator.Send(new CheckPostsCommand() { Posts = instaPosts, Participants = instaParticipants });

        await _mediator.Send(new CreateCheckResultsCommand());
    }
}
