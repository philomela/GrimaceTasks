using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class TaskConfiguration : IEntityTypeConfiguration<Domain.Core.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Core.Task> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
