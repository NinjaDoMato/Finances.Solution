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
    public class InvestmentFaker : EntityFaker<Investment>
    {
        private ReserveInvestmentFaker reserveInvestmentFaker;
        public InvestmentFaker()
        {
            faker = new Faker<Investment>()
                    .RuleFor(e => e.Id, f => new Guid())
                    .RuleFor(e => e.Name, f => f.Lorem.Word())
                    .RuleFor(e => e.Rentability, f => f.Random.Decimal(0.1m, 15))
                    .RuleFor(e => e.DateCreated, f => f.Date.Past())
                    .RuleFor(e => e.LastUpdate, f => f.Date.Recent())
                    .RuleFor(e => e.StartAmount, f => f.Random.Decimal(1, 1000))
                    .RuleFor(e => e.CurrentAmount, f => f.Random.Decimal(1, 2000))
                    .RuleFor(e => e.Account, f => f.Random.Enum<AccountType>())
                    .RuleFor(e => e.Type, f => f.Random.Enum<InvestmentType>());

        }
    }
}
