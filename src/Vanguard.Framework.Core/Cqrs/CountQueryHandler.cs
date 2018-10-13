namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The total count command handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class CountQueryHandler<TModel, TEntity> : IQueryHandler<int, CountQuery<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IReadRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public CountQueryHandler(IReadRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public virtual int Retrieve(CountQuery<TModel> query)
        {
            return _repository.GetCount(query.Criteria);
        }
    }
}
