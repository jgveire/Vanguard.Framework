using AutoMapper;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The get command handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IQueryHandler{TResult, TQuery}" />
    public class GetQueryHandler<TModel, TEntity> : IQueryHandler<TModel, GetQuery<TModel>>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQueryHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public GetQueryHandler(IRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
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

            var model = Mapper.Map<TModel>(entity);
            return model;
        }
    }
}
