using System.Threading.Tasks;

namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IDataEntity
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int Save();

        /// <summary>
        /// Saves all changes asynchronously.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveAsync();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);
    }
}
