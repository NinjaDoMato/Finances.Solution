using Finances.Tests.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.RepositoryTests
{
    [TestClass]
    public class EntryTests : TestBase<Entry>
    {
        public EntryTests() : base(new EntryFaker()) { }

        protected override List<string> GetIncludes()
        {
            return new List<string>
            {
                { nameof(Entry.Reserve) }
            };
        }

        protected override bool ValidateEntity(Entry entity)
        {
            return entity.Reserve != null;
        }
    }
}
