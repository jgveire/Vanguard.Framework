namespace ExampleService
{
    using System;
    using System.Buffers;
    using System.Linq;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using AutoMapper;
    using ExampleData;
    using ExampleData.Entities;
    using ExampleModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using Vanguard.Framework.Http.Filters;
    using Vanguard.Framework.Http.Formatters;

    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The hosting environment</param>
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

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        /// <param name="app">The application.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(SetupSwaggerUi);
            app.UseDeveloperExceptionPage();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </remarks>
        /// <param name="services">The service collection.</param>
        /// <returns>The service provider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidationExceptionFilter));
                    options.Filters.Add(typeof(ValidateModelAttribute));
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(new SelectFieldJsonOutputFormatter(JsonSerializerSettingsProvider.CreateSerializerSettings(), ArrayPool<char>.Shared));
                });

            services.AddDbContext<ExampleContext>(options => options.UseInMemoryDatabase("Example"));
            services.AddSwaggerGen(SetupSwagger);

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            InitContext(container.Resolve<ExampleContext>());

            return new AutofacServiceProvider(container);
        }

        private static void SetupSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info { Title = "Example API", Version = "v1" });
        }

        private static void SetupSwaggerUi(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API V1");
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
