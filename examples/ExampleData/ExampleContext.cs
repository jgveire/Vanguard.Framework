namespace ExampleData
{
    using ExampleData.Entities;
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
