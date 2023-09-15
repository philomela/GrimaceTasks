using Domain.Core;
using Domain.Core.Dicitionarys;
using Domain.Interfaces;
using MediatR;

namespace Application.Participants.Queries;

public record GetParticipantsBySocialNetworkQuery : IRequest<List<Participant>>
{
    public string SocialNetworkName { get; set; }
}

public class GetParticipantsBySocialNetworkQueryHandler : IRequestHandler<GetParticipantsBySocialNetworkQuery, List<Participant>>
{
    private readonly IAppDbContext _appDbContext;

    public GetParticipantsBySocialNetworkQueryHandler(IAppDbContext appDbContext)
        => _appDbContext = appDbContext; 

    public async Task<List<Participant>> Handle(GetParticipantsBySocialNetworkQuery request, CancellationToken cancellationToken)
        => _appDbContext.Participants
        .ToList();

}
