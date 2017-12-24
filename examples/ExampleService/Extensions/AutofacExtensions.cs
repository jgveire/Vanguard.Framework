namespace ExampleService.Extensions
{
    using System.Collections.Generic;
    using Autofac;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Core.Repositories;

    /// <summary>
    /// The Autofac extensions class.
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// Registers the CRUD command and query handlers for an entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The Autofac container builder.</param>
        public static void RegisterCrudHandlers<TModel, TEntity>(this ContainerBuilder builder)
            where TEntity : class, IDataEntity
        {
            builder.RegisterType<GetQueryHandler<TModel, TEntity>>().As<IQueryHandler<TModel, GetQuery<TModel>>>();
            builder.RegisterType<CountQueryHandler<TModel, TEntity>>().As<IQueryHandler<int, CountQuery<TModel>>>();
            builder.RegisterType<FindQueryHandler<TModel, TEntity>>().As<IQueryHandler<IEnumerable<TModel>, FindQuery<TModel>>>();
            builder.RegisterType<DeleteCommandHandler<TModel, TEntity>>().As<ICommandHandler<DeleteCommand<TModel>>>();
            builder.RegisterType<AddCommandHandler<TModel, TEntity>>().As<ICommandHandler<AddCommand<TModel>>>();
            builder.RegisterType<UpdateCommandHandler<TModel, TEntity>>().As<ICommandHandler<UpdateCommand<TModel>>>();
        }
    }
}
