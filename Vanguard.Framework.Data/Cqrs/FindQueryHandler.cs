using System.Collections.Generic;
using AutoMapper;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The find query handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class FindQueryHandler<TModel, TEntity> : IQueryHandler<IEnumerable<TModel>, FindQuery<TModel>>
            where TEntity : class, IEntity
    {
        private readonly IReadRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public FindQueryHandler(IReadRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Retrieves the result for the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public IEnumerable<TModel> Retrieve(FindQuery<TModel> query)
        {
            var entities = _repository.Find(query.Criteria);
            var model = Mapper.Map<IEnumerable<TModel>>(entities);
            return model;
        }
    }
}