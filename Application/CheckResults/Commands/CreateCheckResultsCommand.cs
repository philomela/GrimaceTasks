using Domain.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.CheckResults.Commands;

public record CreateCheckResultsCommand : IRequest<Unit>
{
    public Dictionary<long, List<Participant>> CheckResults { get; set; }
}

public class CreateCheckResultsCommandHandler : IRequestHandler<CreateCheckResultsCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;

    public CreateCheckResultsCommandHandler(IAppDbContext appDbContext) 
        => _appDbContext = appDbContext;

    public async Task<Unit> Handle(CreateCheckResultsCommand request, CancellationToken cancellationToken)
    {
        var checkResults = request.CheckResults.Select(cr => new Domain.Core.CheckResults()
        {

        }).ToList();

        return Unit.Value;
    }
}
