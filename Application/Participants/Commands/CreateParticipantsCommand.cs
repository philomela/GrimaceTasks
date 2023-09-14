using Application.Common.RestApi;
using Domain.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Participants.Commands;

[ParticipantsRestApi]
public record CreateParticipantsCommand : IRequest<Unit>
{
    public List<Participant> Participants { get; set; }
}

public class CreateParticipantsCommandHandler : IRequestHandler<CreateParticipantsCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;

    public CreateParticipantsCommandHandler(IAppDbContext appDbContext) => _appDbContext = appDbContext; 
    public async Task<Unit> Handle(CreateParticipantsCommand request, CancellationToken cancellationToken)
    {
        var participantIds = request.Participants
            .Select(p => p.Id)
            .ToList();

        var existingParticipants = _appDbContext.Participants
            .Where(p => participantIds.Contains(p.Id))
            .ToList();

        var participantsToAdd = request.Participants
            .Where(p => !existingParticipants
            .Contains(p))
            .ToList();

        await _appDbContext.Participants
            .AddRangeAsync(participantsToAdd, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
