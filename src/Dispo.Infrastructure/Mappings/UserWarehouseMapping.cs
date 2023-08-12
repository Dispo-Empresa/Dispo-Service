using Dispo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dispo.Infrastructure.Mappings
{
    internal class UserWarehouseMapping : IEntityTypeConfiguration<UserWarehouse>
    {
        public void Configure(EntityTypeBuilder<UserWarehouse> builder)
        {
            builder.ToTable("UserWarehouses");

            builder.HasKey(sc => new { sc.UserId, sc.WarehouseId });
        }
    }
}
