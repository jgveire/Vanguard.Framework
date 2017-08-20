using AutoMapper;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The add command handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand}" />
    public class AddCommandHandler<TModel, TEntity> : ICommandHandler<AddCommand<TModel>>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommandHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public AddCommandHandler(IRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void Execute(AddCommand<TModel> command)
        {
            var entity = Mapper.Map<TEntity>(command.Model);
            _repository.Add(entity);
        }
    }
}
