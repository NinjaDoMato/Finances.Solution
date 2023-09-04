using Finances.Database.Entities;
using System.Linq.Expressions;

namespace Finances.Database.Repository;

/// <summary>
/// The base repository to access the database. 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Find the first entity with the given Id. If no entity match the given Id, and exception will be thrown.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity Find(Guid id);

    /// <summary>
    /// Find the first entity with the given Id async. If no entity match the given Id, and exception will be thrown.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Guid id);

    /// <summary>
    /// Insert an entity in the database and save the changes.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an entity in the database and save the changes.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Removes an entity from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Return the first entity from the database that matches the condition, if a condition is not passed returns the first entity found. A exception will be thrown if no entity matches the conditon.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    TEntity First(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);

    /// <summary>
    /// Return the first entity from the database that matches the condition, if a condition is not passed returns the first entity found. A exception will be thrown if no entity matches the conditon.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);

    /// <summary>
    /// Return the first entity from the database that matches the condition, if a condition is not passed returns the first entity found. If no entity matches the condition, a null object will be returned.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);

    /// <summary>
    /// Return the first entity from the database that matches the condition, if a condition is not passed returns the first entity found. If no entity matches the condition, a null object will be returned.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);


    /// <summary>
    /// Return a list of entities that match the given condition, if no condition is passed a ICollction with all the entities will be returned.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);

    /// <summary>
    /// Return a list of entities that match the given condition, if no condition is passed a ICollction with all the entities will be returned.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ICollection<TEntity> Where(Expression<Func<TEntity, bool>>? expression = null, List<string>? includeProperties = null);

    /// <summary>
    /// Returns all of the entries of the table. Use with wisdom.
    /// </summary>
    /// <returns></returns>
    ICollection<TEntity> All();

    /// <summary>
    /// Returns all of the entries of the table. Use with wisdom.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<TEntity>> AllAsync();

    /// <summary>
    /// Detach the given entity from the change tracker in the current context.
    /// </summary>
    /// <param name="entity"></param>
    void DetachEntity(TEntity entity);
}
