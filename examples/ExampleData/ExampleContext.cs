namespace ExampleData
{
    using ExampleData.Entities;
    using Microsoft.EntityFrameworkCore;
    using Vanguard.Framework.Core.DomainEvents;
    using Vanguard.Framework.Data;

    public class ExampleContext : DbContextBase
    {
        public ExampleContext(IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
        {
        }

        public ExampleContext(IEventDispatcher eventDispatcher, DbContextOptions<ExampleContext> options)
            : base(eventDispatcher, options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
