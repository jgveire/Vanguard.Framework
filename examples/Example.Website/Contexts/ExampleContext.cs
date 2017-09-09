using Microsoft.EntityFrameworkCore;
using Vanguard.Framework.Website.Entities;

namespace Vanguard.Framework.Website.Contexts
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
