﻿using Finances.Database.Configurations;
using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finances.Database.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<Reserve> Reserves { get; set; }
    public virtual DbSet<Investment> Investments { get; set; }
    public virtual DbSet<ReserveInvestment> ReserveInvestmentMaps { get; set; }
    public virtual DbSet<Entry> Entries { get; set; }
    public virtual DbSet<Cost> Costs { get; set; }
    public virtual DbSet<Payment> CostPayments { get; set; }
    public virtual DbSet<Purchase> Purchases { get; set; }
    public virtual DbSet<Installment> PurchaseInstallments { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<IncomeSource> IncomeSources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveInvestmentConfiguration());
        modelBuilder.ApplyConfiguration(new IncomeSourceConfiguration());

        // Configuração do usuário inicial
        var initialUser = new User
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = "daniel.deda1995@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("ddd599123"),
            DateCreated = DateTime.UtcNow
        };

        modelBuilder.Entity<User>().HasData(initialUser);
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
