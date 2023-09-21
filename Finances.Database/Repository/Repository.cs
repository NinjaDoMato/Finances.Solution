using Finances.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finances.Database.Repository
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> setDb;

        public Repository(DbContext context)
        {
            this.context = context;
            this.setDb = context.Set<TEntity>();
        }

        public TEntity Find(Guid id)
        {
            return this.setDb.Find(id);
        }

        public async Task<TEntity> FindAsync(Guid id)
        {
            return await this.setDb.FindAsync(id);
        }

        public TEntity First(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return query.First();
        }
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return await query.FirstAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return query.FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = this.setDb.Find(id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with Id {id} was not found.");
            }

            setDb.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await setDb.AddAsync(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (await setDb.AnyAsync(e => e.Id == entity.Id))
            {
                setDb.Update(entity);

                await context.SaveChangesAsync();

                return entity;
            }

            throw new KeyNotFoundException($"Entity with id {entity.Id} was not foud.");
        }

        public ICollection<TEntity> Where(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return query.ToList();
        }

        public async Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            var query = ApplyExpression(expression, includeProperties);

            return await query.ToListAsync();
        }

        private IQueryable<TEntity> ApplyExpression(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null)
        {
            IQueryable<TEntity> query = setDb.AsNoTracking();

            if (includeProperties != null)
            {
                foreach (string property in includeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public ICollection<TEntity> All()
        {
            return setDb.AsNoTracking().ToList();
        }

        public async Task<ICollection<TEntity>> AllAsync()
        {
            return await setDb.AsNoTracking().ToListAsync();
        }

        public void DetachEntity(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }
    }
}
