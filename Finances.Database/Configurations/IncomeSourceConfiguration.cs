using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Database.Configurations;

public class IncomeSourceConfiguration : IEntityTypeConfiguration<IncomeSource>
{
    public void Configure(EntityTypeBuilder<IncomeSource> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Owner)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
} 