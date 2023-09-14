using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class PostConfiguration : IEntityTypeConfiguration<Domain.Core.Post>
{
    public void Configure(EntityTypeBuilder<Domain.Core.Post> builder)
    {
        builder.ToTable("Posts", "Grimace");
        builder.HasKey(x => x.Id);
    }
}
