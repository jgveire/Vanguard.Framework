namespace Example.WebApi.Contexts
{
    using Example.WebApi.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
