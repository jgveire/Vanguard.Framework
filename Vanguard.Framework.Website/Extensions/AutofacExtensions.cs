using System.Collections.Generic;
using Autofac;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;
using Vanguard.Framework.Data.Cqrs;

namespace Vanguard.Framework.Website.Extensions
{
    public static class AutofacExtensions
    {
        public static void RegisterCrudCommandAndQueryHandlers<TModel, TEntity>(this ContainerBuilder builder)
            where TEntity : class, IEntity
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
