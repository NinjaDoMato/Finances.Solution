using Finances.Database.Configurations;
using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Database.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<Reserve> Reserves { get; set; }
    public virtual DbSet<Investment> Investments { get; set; }
    public virtual DbSet<ReserveInvestment> ReserveInvestmentMaps { get; set; }
    public virtual DbSet<Entry> Entries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveInvestmentConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = "server=localhost;database=finances;user=root;password=!root";

        optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.DateCreated = now;
                        entity.LastUpdate = now;
                        break;

                    case EntityState.Modified:
                        entity.LastUpdate = now;
                        break;
                }
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.DateCreated = now;
                        entity.LastUpdate = now;
                        break;

                    case EntityState.Modified:
                        entity.LastUpdate = now;
                        break;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
