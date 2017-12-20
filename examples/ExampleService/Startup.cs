using System.Linq;

namespace ExampleService
{
    using System;
    using System.Buffers;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using ExampleData;
    using ExampleData.Entities;
    using ExampleService.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Vanguard.Framework.Http.Filters;
    using Vanguard.Framework.Http.Formatters;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Mapper.Initialize(config =>
                {
                    config.CreateMap<Car, CarModel>();
                    config.CreateMap<CarModel, Car>();
                    config.CreateMap<Garage, GarageModel>();
                    config.CreateMap<GarageModel, Garage>();
                });

            ////var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            ////jsonFormatter.SerializerSettings.ContractResolver = new SelectFieldContractResolver()
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ExceptionFilter));
                    options.Filters.Add(typeof(ValidateModelAttribute));
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(new SelectFieldJsonOutputFormatter(JsonSerializerSettingsProvider.CreateSerializerSettings(), ArrayPool<char>.Shared));
                });

            services.AddDbContext<ExampleContext>(options => options.UseInMemoryDatabase("Example"));

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            InitContext(container.Resolve<ExampleContext>());

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseDeveloperExceptionPage();
        }

        private void InitContext(ExampleContext context)
        {
            if (!context.Cars.Any())
            {
                var car = new Car(new Guid("9f490218-7db0-452f-8809-a390f9ca5a95"), "BMW", "Z5", "14-ZXD-5", 29999.99m);
                car.ReportStolen();
                context.Cars.Add(car);
                context.Cars.Add(new Car(new Guid("5b2ca291-9fd6-4b36-abaf-4af27a64f322"), "Audi", "A4", "63-KDI-8", 34000));
                context.Cars.Add(new Car(new Guid("e8289985-aeaf-4867-85ab-af354c0d1d85"), "Audi", "A5", "03-UWE-3", 39000));
                context.Cars.Add(new Car(new Guid("fa47ff63-f5b7-4697-beae-6bb8384fd5b2"), "Opel", "Astra", "92-WUD-2", 20000));

                context.Garages.Add(new Garage("B.I.G. Garage", "Black Pool 56, Manchester"));
                context.Garages.Add(new Garage("S.I.M. Garage", "West Lake 23, London"));

                context.SaveChanges();
            }
        }
    }
}
