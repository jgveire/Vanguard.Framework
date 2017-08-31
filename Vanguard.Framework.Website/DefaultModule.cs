using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Data.Repositories;
using Vanguard.Framework.Website.Contexts;
using Vanguard.Framework.Website.Entities;
using Vanguard.Framework.Website.Extensions;
using Vanguard.Framework.Website.Models;

namespace Vanguard.Framework.Website
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<QueryDispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReadRepository<Car>>().AsImplementedInterfaces();
            builder.RegisterType<ExampleContext>().As<DbContext>();

            builder.RegisterCrudCommandAndQueryHandlers<CarModel, Car>();
        }
    }
}
