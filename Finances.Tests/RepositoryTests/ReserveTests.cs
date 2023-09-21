using Finances.Tests.Fakers;

namespace Finances.Tests.RepositoryTests
{
    [TestClass]
    public class ReserveTests : TestBase<Reserve>
    {
        public ReserveTests() : base(new ReserveFaker()) { }

        protected override List<string> GetIncludes()
        {
            return new List<string>()
            {
                { nameof(Reserve.Entries) }
            };
        }

        protected override bool ValidateEntity(Reserve entity)
        {
            return !string.IsNullOrEmpty(entity.Name) &&
                   !string.IsNullOrEmpty(entity.Description) &&
                   entity.Entries != null &&
                   entity.Entries.Count > 0;
        }
    }
}
