namespace ExampleService
{
    using Autofac;
    using ExampleData;
    using ExampleData.Entities;
    using ExampleService.Extensions;
    using ExampleService.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Data.Repositories;

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
