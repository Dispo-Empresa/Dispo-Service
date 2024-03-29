﻿using Dispo.Shared.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dispo.Shared.Infrastructure.Persistence.Mappings
{
    public class ProductDimensionMapping : IEntityTypeConfiguration<ProductDimension>
    {
        public void Configure(EntityTypeBuilder<ProductDimension> builder)
        {
            builder.ToTable("ProductDimensions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn()
                   .HasColumnType("BIGINT")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Weight)
                   .HasColumnName("Weight")
                   .HasColumnType("DECIMAL(10, 2)")
                   .HasMaxLength(120);

            builder.Property(x => x.Height)
                   .HasColumnName("Height")
                   .HasColumnType("DECIMAL(10, 2)")
                   .HasMaxLength(120);

            builder.Property(x => x.Width)
                   .HasColumnName("Width")
                   .HasColumnType("DECIMAL(10, 2)")
                   .HasMaxLength(120);

            builder.Property(x => x.Depth)
                   .HasColumnName("Depth")
                   .HasColumnType("DECIMAL(10, 2)")
                   .HasMaxLength(120);
        }
    }
}