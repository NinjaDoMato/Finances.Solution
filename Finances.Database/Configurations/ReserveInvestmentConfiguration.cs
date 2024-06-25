using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Database.Configurations;

public class ReserveInvestmentConfiguration : IEntityTypeConfiguration<ReserveInvestment>
{
    public void Configure(EntityTypeBuilder<ReserveInvestment> builder)
    {
        builder.ToTable("ReserveInvestmentsMaps");
        builder.HasKey(e => new { e.ReserveId, e.InvestmentId });

        builder.HasOne(e => e.Reserve)
            .WithMany(p => p.LinkedInvestments)
            .HasForeignKey(e => e.ReserveId);

        builder.HasOne(e => e.Investment)
            .WithMany(c => c.SourceReserves)
            .HasForeignKey(e => e.InvestmentId);
    }
}