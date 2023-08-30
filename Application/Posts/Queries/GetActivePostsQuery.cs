using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries;

public record GetActivePostsQuery : IRequest<List<Post>>
{
}

public class GetActivePostsQueryHandler : IRequestHandler<GetActivePostsQuery, List<Post>>
{
    //Забираем все задачи которые активные, и по которым нужно проверять логины на выпонимость.
    public Task<List<Post>> Handle(GetActivePostsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
