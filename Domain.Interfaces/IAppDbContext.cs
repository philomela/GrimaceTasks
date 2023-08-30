using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IAppDbContext
{
    public DbSet<Participant> Participants { get; set; }

    public DbSet<Domain.Core.Task> Tasks { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}