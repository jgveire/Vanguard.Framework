namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Exceptions;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Core.Resources;

    /// <summary>
    /// The update command handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand}" />
    public class UpdateCommandHandler<TModel, TEntity> : ICommandHandler<UpdateCommand<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommandHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        /// <param name="mapper">The entity mapper.</param>
        public UpdateCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = Guard.ArgumentNotNull(repository, nameof(repository));
            _mapper = Guard.ArgumentNotNull(mapper, nameof(mapper));
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void Execute(UpdateCommand<TModel> command)
        {
            var entity = _repository.GetById(command.Id);
            if (entity == null)
            {
                var message = string.Format(ExceptionResource.CannotFindEntity, command.Id);
                throw new DataException(message);
            }

            _mapper.Map(command.Model, entity);
            _repository.Update(entity);
            _repository.Save();
        }
    }
}
