using Bogus;
using Finances.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.Fakers
{
    public abstract class EntityFaker<T> where T : BaseEntity
    {
        public Faker<T> faker;

        public List<T> Generate(int count)
        {
            return faker.GenerateLazy(count).ToList();
        }

        public T GenerateOne()
        {
            return faker.GenerateLazy(1).First();
        }
    }
}
