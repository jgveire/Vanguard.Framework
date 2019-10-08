namespace Vanguard.Framework.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The repository base class.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public class RepositoryBase<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RepositoryBase(TDbContext dbContext)
        {
            DbContext = Guard.ArgumentNotNull(dbContext, nameof(dbContext));
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected TDbContext DbContext { get; }
    }
}
