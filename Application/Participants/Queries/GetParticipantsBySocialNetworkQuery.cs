using Domain.Core;
using MediatR;

namespace Application.Participants.Queries;

public record GetParticipantsBySocialNetworkQuery : IRequest<List<Participant>>
{
}

public class GetParticipantsBySocialNetworkQueryHandler : IRequestHandler<GetParticipantsBySocialNetworkQuery, List<Participant>>
{
    //Сдесь получаем логины по переданному socialNetworkName, если оно null or empty, ищем всех пользователей.
    public Task<List<Participant>> Handle(GetParticipantsBySocialNetworkQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
