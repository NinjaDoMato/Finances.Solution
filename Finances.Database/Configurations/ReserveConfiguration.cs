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

public class ReserveConfiguration : IEntityTypeConfiguration<Reserve>
{
    public void Configure(EntityTypeBuilder<Reserve> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

        builder.HasMany(e => e.Entries)
               .WithOne(e => e.Reserve);
    }
}