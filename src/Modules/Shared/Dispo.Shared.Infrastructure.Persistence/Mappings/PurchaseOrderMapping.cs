﻿using Dispo.Shared.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dispo.Shared.Infrastructure.Persistence.Mappings
{
    public class PurchaseOrderMapping : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn()
                   .HasColumnType("BIGINT")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Number)
                   .IsRequired()
                   .HasColumnName("Number")
                   .HasColumnType("VARCHAR(20)")
                   .HasMaxLength(20);

            builder.Property(x => x.CreationDate)
                   .IsRequired()
                   .HasColumnName("Date")
                   .HasColumnType("datetime2");

            builder.Property(x => x.PaymentMethod)
                   .IsRequired()
                   .HasColumnName("PaymentMethod")
                   .HasColumnType("SMALLINT")
                   .HasMaxLength(120);

            builder.Property(x => x.NotificationType)
                   .IsRequired()
                   .HasColumnName("NotificationType")
                   .HasColumnType("SMALLINT")
                   .HasMaxLength(120);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasColumnName("Status")
                   .HasColumnType("SMALLINT")
                   .HasMaxLength(120);

            builder.HasOne(a => a.Warehouse)
                   .WithMany(b => b.PurchaseOrders)
                   .HasForeignKey(x => x.WarehouseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Supplier)
                   .WithMany(a => a.PurchaseOrders)
                   .HasForeignKey(c => c.SupplierId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}