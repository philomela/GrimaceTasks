using MediatR;

namespace Application.CheckResults.Commands;

public record CreateCheckResultsCommand : IRequest<Unit>
{

}

public class CreateCheckResultsCommandHandler : IRequestHandler<CreateCheckResultsCommand, Unit>
{
    public async Task<Unit> Handle(CreateCheckResultsCommand request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}
