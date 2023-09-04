using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Finances.Database.Entities;
using System.Reflection.Emit;

namespace Finances.Database.Configurations;

public class ReserveInvestmentConfiguration : IEntityTypeConfiguration<ReserveInvestment>
{
    public void Configure(EntityTypeBuilder<ReserveInvestment> builder)
    {
        builder.HasKey(e => new { e.ReserveId, e.InvestmentId });

        builder.HasOne(e => e.Reserve)
            .WithMany(p => p.LinkedInvestments)
            .HasForeignKey(e => e.ReserveId);

        builder.HasOne(e => e.Investment)
            .WithMany(c => c.SourceReserves)
            .HasForeignKey(e => e.InvestmentId);
    }
}