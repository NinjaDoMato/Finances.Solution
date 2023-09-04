using Finances.Database.Context;
using Finances.Database.Entities;
using Finances.Database.Repository;
using Finances.Tests.Context;
using Finances.Tests.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.Tests.RepositoryTests;

public abstract class TestBase<T> where T : BaseEntity
{
    protected readonly DatabaseContext dbContext;
    protected readonly IRepository<T> repository;
    protected EntityFaker<T> faker;

    public TestBase(EntityFaker<T> faker)
    {
        dbContext = new InMemoryContext().Context;
        repository = new Repository<T>(dbContext);
        this.faker = faker;
    }

    [TestMethod]
    public void FirstOrDefault()
    {
        var data = repository.FirstOrDefault(e => e.Id != new Guid(), GetIncludes());

        Assert.IsNotNull(data);

        Assert.IsTrue(ValidateEntity(data));
    }

    [TestMethod]
    public async Task FirstOrDefaultAsync()
    {
        var data = await repository.FirstOrDefaultAsync(e => e.Id != new Guid(), GetIncludes());

        Assert.IsNotNull(data);

        Assert.IsTrue(ValidateEntity(data));
    }

    [TestMethod]
    public async Task WhereAsync()
    {
        var dataList = await repository.WhereAsync(e => e.Id != new Guid(), GetIncludes());

        Assert.IsNotNull(dataList);
        Assert.IsTrue(dataList.Count > 0);

        foreach (var data in dataList)
        {
            Assert.IsTrue(ValidateEntity(data));
        }
    }

    [TestMethod]
    public void Where()
    {
        var dataList = repository.Where(e => e.Id != new Guid(), GetIncludes());

        Assert.IsNotNull(dataList);
        Assert.IsTrue(dataList.Count > 0);

        foreach (var data in dataList)
        {
            Assert.IsTrue(ValidateEntity(data));
        }
    }

    [TestMethod]
    public async Task InsertAsync()
    {
        var testData = faker.GenerateOne();
        var count = repository.All().Count();
        
        var result = await repository.InsertAsync(testData);

        Assert.IsNotNull(result);
        Assert.IsTrue(count < repository.All().Count());
        
        Assert.IsTrue(ValidateEntity(result));
    }

    protected abstract List<string> GetIncludes();
    protected abstract bool ValidateEntity(T entity);

}
