using Bogus;

namespace Finances.Tests.Fakers;

public class CostFaker : EntityFaker<Cost>
{
    public CostFaker()
    {
        faker = new Faker<Cost>()
                .RuleFor(e => e.Id, f => new Guid())
                .RuleFor(e => e.DateCreated, f => f.Date.Past())
                .RuleFor(e => e.LastUpdate, f => f.Date.Recent())
                .RuleFor(e => e.Amount, f => f.Random.Decimal(1, 100))
                .RuleFor(e => e.Amount, f => f.Random.Decimal(-100, 100));
    }
}
