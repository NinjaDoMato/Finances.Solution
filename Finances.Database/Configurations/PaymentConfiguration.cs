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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.PaidAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DatePaid)
                .HasColumnType("datetime");

            builder.HasOne(p => p.Cost)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
