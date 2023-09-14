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
    {
        return await Task.Run<List<Participant>>(() =>

            new List<Participant>
            {
                //new Participant
                //{
                //    Id = 1,
                //    UserName = "grimaceodisei",
                //    SocialNetworks = SocialNetworks.Instagram
                //},
                //new Participant
                //{
                //    Id = 2,
                //    UserName = "crankymonk",
                //    SocialNetworks = SocialNetworks.Instagram
                //},
                //new Participant
                //{
                //    Id = 3,
                //    UserName = "laertigor",
                //    SocialNetworks = SocialNetworks.Instagram
                //},new Participant
                //{
                //    Id = 4,
                //    UserName = "fullplaycrypto",
                //    SocialNetworks = SocialNetworks.Instagram
                //},new Participant
                //{
                //    Id = 5,
                //    UserName = "glebkoo777",
                //    SocialNetworks = SocialNetworks.Instagram
                //},new Participant
                //{
                //    Id = 6,
                //    UserName = "__l__is__",
                //    SocialNetworks = SocialNetworks.Instagram
                //}
            }
        );
    }
}
