﻿using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Database.Configurations;

public class CostConfiguration : IEntityTypeConfiguration<Cost>
{
    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.ToTable("Costs");
        builder.HasMany(e => e.Payments)
               .WithOne(e => e.Cost)
               .HasForeignKey(e => e.CostId);

        builder.HasOne(e => e.Reserve)
               .WithMany()
               .HasForeignKey(e => e.ReserveId)
               .IsRequired(false);
    }
}