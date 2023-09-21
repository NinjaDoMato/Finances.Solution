using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Database.Configurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.HasOne(e => e.Reserve)
               .WithMany(e => e.Entries)
               .HasForeignKey(e => e.ReserveId);
    }
}