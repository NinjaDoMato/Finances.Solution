using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Database.Configurations
{
    public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.ToTable("PurchaseInstallments");

            builder.Property(i => i.Amount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(i => i.InstallmentNumber)
                .IsRequired();

            builder.Property(i => i.Paid)
                .IsRequired();

            builder.Property(i => i.DueDate)
                .IsRequired();

            builder.Property(i => i.PaidDate);

            builder.Property(i => i.PaymentUrl)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(i => i.Purchase)
                .WithMany(p => p.Installments)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
