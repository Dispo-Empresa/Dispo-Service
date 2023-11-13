using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dispo.Shared.Infrastructure.Persistence.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Core.Domain.Entities.Account>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn()
                   .HasColumnType("BIGINT")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasDefaultValue(true);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasColumnName("Email")
                   .HasColumnType("VARCHAR(220)")
                   .HasMaxLength(220);

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasColumnName("Password")
                   .HasColumnType("VARCHAR(255)")
                   .HasMaxLength(255);

            builder.Property(x => x.LastLicenceCheck)
                   .HasColumnName("LastLicenceCheck")
                   .HasColumnType("datetime2");

            builder.Property(x => x.CompanyIdByHub)
                   .IsRequired()
                   .HasColumnName("CompanyIdByHub")
                   .HasColumnType("BIGINT");

            builder.Property(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(a => a.Role)
                   .WithMany(b => b.Accounts)
                   .HasForeignKey(c => c.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                   .WithOne(b => b.Account)
                   .HasForeignKey<Core.Domain.Entities.Account>(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}