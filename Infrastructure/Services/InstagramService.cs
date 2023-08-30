using Application.Common.Interfaces;
using Domain.Core;

namespace Infrastructure.Services;

public class InstagramService : IInstagramService<Post, Participant>
{
    async public Task<Post> CheckPostAsync(List<Participant> participant)
    {
        return await Task.Run<Post>(() => new Post());
    }
}
