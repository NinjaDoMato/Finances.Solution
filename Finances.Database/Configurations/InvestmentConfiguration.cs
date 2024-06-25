using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Database.Configurations
{
    public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
    {
        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            // Configure table name
            builder.ToTable("Investments");

            // Configure primary key
            builder.HasKey(i => i.Id);

            // Configure relationships
            builder.HasMany(i => i.SourceReserves)
                .WithOne(r => r.Investment)
                .HasForeignKey(r => r.InvestmentId);
        }
    }
}
