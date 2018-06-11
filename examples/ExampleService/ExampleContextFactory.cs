namespace ExampleService
{
    using ExampleData;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// The example context factory.
    /// </summary>
    public class ExampleContextFactory : IDesignTimeDbContextFactory<ExampleContext>
    {
        /// <inheritdoc />
        public ExampleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Example;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ExampleContext(new DummyEventDispatcher(), optionsBuilder.Options);
        }
    }
}
