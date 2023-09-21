using Finances.Database.Context;
using Finances.Tests.Fakers;
using Microsoft.EntityFrameworkCore;

namespace Finances.Tests.Context;

public class InMemoryContext : IDisposable
{
    public DatabaseContext Context { get; }

    public DbContextOptions<DatabaseContext> ContextOptions { get; }

    public InMemoryContext()
    {

        ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                     .UseInMemoryDatabase("testBase")
                     .EnableSensitiveDataLogging()
                     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                     .Options;

        Context = new DatabaseContext(ContextOptions);
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();

        ReserveFaker reserveFaker = new();
        InvestmentFaker investmentFaker = new();

        List<Investment> investments = investmentFaker.faker.Generate(10);

        foreach (var investment in investments)
        {
            investment.SourceReserves.Add(new()
            {
                Investment = investment,
                Reserve = reserveFaker.faker.Generate(1).First(),
            });
        }

        //Context.Entries.AddRange(entryFaker.faker.Generate(5));
        //Context.Reserves.AddRange(reserveFaker.faker.Generate(5));
        Context.Investments.AddRange(investments);


        Context.SaveChanges();
    }

    public void Dispose()
    {
        if (Context != null)
        {
            Context.Dispose();
        }
    }
}