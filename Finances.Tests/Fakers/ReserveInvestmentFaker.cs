using Bogus;
using Finances.Database.Entities;
using Finances.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.Fakers
{
    public class ReserveInvestmentFaker : EntityFaker<ReserveInvestment>
    {
        private ReserveFaker reserveFaker;
        private InvestmentFaker investmentFaker;
        public ReserveInvestmentFaker()
        {
            reserveFaker = new();
            investmentFaker = new();

            faker = new Faker<ReserveInvestment>()
                    .RuleFor(e => e.Id, f => new Guid())
                    .RuleFor(e => e.DateCreated, f => f.Date.Past())
                    .RuleFor(e => e.LastUpdate, f => f.Date.Recent())
                    .RuleFor(e => e.Investment, f => investmentFaker.faker.Generate(1).First())
                    .RuleFor(e => e.Reserve, f => reserveFaker.faker.Generate(1).First());

        }
    }
}
