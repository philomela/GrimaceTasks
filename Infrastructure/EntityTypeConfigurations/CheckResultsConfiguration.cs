using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class CheckResultsConfiguration : IEntityTypeConfiguration<CheckResults>
{
    public void Configure(EntityTypeBuilder<CheckResults> builder)
    {
        builder.ToTable("CheckResults", "Grimace");
        builder.HasKey(x => x.Id);
    }
}
