namespace Vanguard.Framework.Core.Cqrs
{
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The add command handler.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand}" />
    public class AddCommandHandler<TModel, TEntity> : ICommandHandler<AddCommand<TModel>>
        where TEntity : class, IDataEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommandHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        /// <param name="mapper">The entity mapper.</param>
        public AddCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            Guard.ArgumentNotNull(mapper, nameof(mapper));
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void Execute(AddCommand<TModel> command)
        {
            var entity = _mapper.Map<TEntity>(command.Model);
            _repository.Add(entity);
            _repository.Save();
        }
    }
}
