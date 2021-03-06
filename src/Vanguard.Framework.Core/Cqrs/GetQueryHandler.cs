﻿namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The get command handler.
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
            _repository = Guard.ArgumentNotNull(repository, nameof(repository));
            _mapper = Guard.ArgumentNotNull(mapper, nameof(mapper));
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
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.
            }

            var model = _mapper.Map<TModel>(entity);
            return model;
        }
    }
}
