using Bogus;
using Finances.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.Fakers;

public class EntryFaker : EntityFaker<Entry>
{
    public EntryFaker()
    {
        faker = new Faker<Entry>()
                .RuleFor(e => e.Id, f => new Guid())
                .RuleFor(e => e.DateCreated, f => f.Date.Past())
                .RuleFor(e => e.LastUpdate, f => f.Date.Recent())
                .RuleFor(e => e.Amount, f => f.Random.Decimal(-100, 100));

    }
}
