using AutoMapper;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Exceptions;
using Vanguard.Framework.Core.Repositories;
using Vanguard.Framework.Data.Resources;

namespace Vanguard.Framework.Data.Cqrs
{
    /// <summary>
    /// The update command handler class.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ICommandHandler{TCommand}" />
    public class UpdateCommandHandler<TModel, TEntity> : ICommandHandler<UpdateCommand<TModel>>
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommandHandler{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The entity repository.</param>
        public UpdateCommandHandler(IRepository<TEntity> repository)
        {
            Guard.ArgumentNotNull(repository, nameof(repository));
            _repository = repository;
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
                string message = string.Format(ExceptionResource.CannotFindEntity, command.Id);
                throw new DataException(message);
            }

            Mapper.Map(command.Model, entity);
            _repository.Update(entity);
            _repository.Save();
        }
    }
}
