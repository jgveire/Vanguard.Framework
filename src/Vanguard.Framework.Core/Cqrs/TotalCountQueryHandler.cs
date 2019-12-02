namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The total count command handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class TotalCountQueryHandler<TModel, TEntity> : IQueryHandler<int, TotalCountQuery<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IReadRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TotalCountQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public TotalCountQueryHandler(IReadRepository<TEntity> repository)
        {
            _repository = Guard.ArgumentNotNull(repository, nameof(repository));
        }

        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public virtual int Retrieve(TotalCountQuery<TModel> query)
        {
            return _repository.GetTotalCount(query.Filter);
        }
    }
}
