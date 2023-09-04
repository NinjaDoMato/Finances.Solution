using Bogus;
using Finances.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.Fakers;

public class ReserveFaker : EntityFaker<Reserve>
{
    private readonly EntryFaker entryFaker;

    public ReserveFaker()
    {
        entryFaker = new();

        faker = new Faker<Reserve>()
                .RuleFor(e => e.Id, f => new Guid())
                .RuleFor(e => e.DateCreated, f => f.Date.Past())
                .RuleFor(e => e.LastUpdate, f => f.Date.Recent())
                .RuleFor(e => e.Description, f => f.Lorem.Sentence())
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Entries, f => entryFaker.faker.Generate(10));
    }
}
