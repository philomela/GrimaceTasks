using Domain.Core;
using Domain.Interfaces;
using Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Domain.Core.Post> Posts { get; set; }

    public DbSet<ApiLog> ApiLogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        //modelBuilder.ApplyConfiguration(new ApiLogConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}