using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Core.Cqrs
{
    /// <summary>
    /// The get command handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class GetQueryHandler<TModel, TEntity> : IQueryHandler<TModel, GetQuery<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IReadRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        /// <param name="mapper">The entity mapper.</param>
        public GetQueryHandler(IReadRepository<TEntity> repository, IMapper mapper)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            Guard.ArgumentNotNull(mapper, nameof(mapper));
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public virtual TModel Retrieve(GetQuery<TModel> query)
        {
            var entity = _repository.GetById(query.Id);
            if (entity == null)
            {
                return default(TModel);
            }

            var model = _mapper.Map<TModel>(entity);
            return model;
        }
    }
}
