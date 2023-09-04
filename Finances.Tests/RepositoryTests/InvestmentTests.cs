using Finances.Tests.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.RepositoryTests
{
    [TestClass]
    public class InvestmentTests : TestBase<Investment>
    {
        public InvestmentTests() : base(new InvestmentFaker()) { }

        protected override List<string> GetIncludes()
        {
            return new List<string>
            {
                { nameof(Investment.SourceReserves) }
            };
        }

        protected override bool ValidateEntity(Investment entity)
        {
            return !string.IsNullOrEmpty(entity.Name) &&
                   entity.Rentability > 0 &&
                   entity.StartAmount > 0;
        }
    }
}
