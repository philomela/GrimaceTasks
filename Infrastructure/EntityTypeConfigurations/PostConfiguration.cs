﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class PostConfiguration : IEntityTypeConfiguration<Domain.Core.Post>
{
    public void Configure(EntityTypeBuilder<Domain.Core.Post> builder)
    {
        builder.HasKey(x => x.Id);
    }
}