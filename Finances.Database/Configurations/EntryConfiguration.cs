using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Finances.Database.Entities;

namespace Finances.Database.Configurations;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id)
              .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Reserve)
               .WithMany(e => e.Entries)
               .HasForeignKey(e => e.ReserveId);
    }
}