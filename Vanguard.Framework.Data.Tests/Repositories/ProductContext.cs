using Microsoft.EntityFrameworkCore;

namespace Vanguard.Framework.Data.Tests.Repositories
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("ProductDb");
            }
        }
    }
}
