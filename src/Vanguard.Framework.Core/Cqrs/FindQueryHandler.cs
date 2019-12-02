namespace Vanguard.Framework.Core.Cqrs
{
    using System.Collections.Generic;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The find query handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class FindQueryHandler<TModel, TEntity> : IQueryHandler<FilterResult<TModel>, FindQuery<TModel>>
            where TEntity : class, IDataEntity
    {
        private readonly IReadRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        /// <param name="mapper">The entity mapper.</param>
        public FindQueryHandler(IReadRepository<TEntity> repository, IMapper mapper)
        {
            _repository = Guard.ArgumentNotNull(repository, nameof(repository));
            _mapper = Guard.ArgumentNotNull(mapper, nameof(mapper));
        }

        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public FilterResult<TModel> Retrieve(FindQuery<TModel> query)
        {
            var entities = _repository.Find(query.Filter);
            var totalCount = _repository.GetTotalCount(query.Filter);
            var items = _mapper.Map<ICollection<TModel>>(entities);
            return new FilterResult<TModel>(items, totalCount);
        }
    }
}