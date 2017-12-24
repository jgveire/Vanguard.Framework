namespace Vanguard.Framework.Data.Tests.Repositories
{
    using Microsoft.EntityFrameworkCore;

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
