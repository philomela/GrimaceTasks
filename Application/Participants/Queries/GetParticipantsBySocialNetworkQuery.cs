using Domain.Core;
using Domain.Core.Dicitionarys;
using MediatR;

namespace Application.Participants.Queries;

public record GetParticipantsBySocialNetworkQuery : IRequest<List<Participant>>
{
}

public class GetParticipantsBySocialNetworkQueryHandler : IRequestHandler<GetParticipantsBySocialNetworkQuery, List<Participant>>
{
    //Сдесь получаем логины по переданному socialNetworkName, если оно null or empty, ищем всех пользователей.
    public async Task<List<Participant>> Handle(GetParticipantsBySocialNetworkQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run<List<Participant>>(() =>

            new List<Participant>
            {
                new Participant
                {
                    Id = 1,
                    UserName = "grimaceodisei",
                    SocialNetworks = SocialNetworks.Instagram
                },
                new Participant
                {
                    Id = 2,
                    UserName = "crankymonk",
                    SocialNetworks = SocialNetworks.Instagram
                },
                new Participant
                {
                    Id = 3,
                    UserName = "laertigor",
                    SocialNetworks = SocialNetworks.Instagram
                },new Participant
                {
                    Id = 4,
                    UserName = "fullplaycrypto",
                    SocialNetworks = SocialNetworks.Instagram
                },new Participant
                {
                    Id = 5,
                    UserName = "glebkoo777",
                    SocialNetworks = SocialNetworks.Instagram
                },new Participant
                {
                    Id = 6,
                    UserName = "__l__is__",
                    SocialNetworks = SocialNetworks.Instagram
                }
            }
        );
    }
}
