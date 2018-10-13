namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The delete command handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand}" />
    public class DeleteCommandHandler<TModel, TEntity> : ICommandHandler<DeleteCommand<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public DeleteCommandHandler(IRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void Execute(DeleteCommand<TModel> command)
        {
            var entity = _repository.GetById(command.Id);
            if (entity == null)
            {
                return;
            }

            _repository.Delete(entity);
            _repository.Save();
        }
    }
}
