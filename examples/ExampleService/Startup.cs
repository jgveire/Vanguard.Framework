namespace ExampleService
{
    using System;
    using System.Linq;
    using System.Text.Json.Serialization;
    using Autofac;
    using AutoMapper;
    using ExampleData;
    using ExampleData.Entities;
    using ExampleModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Mapper.Initialize(config =>
            // {
            //     config.CreateMap<Car, CarModel>();
            //     config.CreateMap<CarModel, Car>();
            //     config.CreateMap<Garage, GarageModel>();
            //     config.CreateMap<GarageModel, Garage>();
            // });
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Example API v1");
            });
        }

        /// <summary>
        /// Configures the container builder.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DefaultModule>();
            var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Example"));
            builder.RegisterInstance(optionsBuilder.Options).As<DbContextOptions<ExampleContext>>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(SetupJson);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });
            });

            // InitContext(services.Resolve<ExampleContext>());
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

        private void SetupJson(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
