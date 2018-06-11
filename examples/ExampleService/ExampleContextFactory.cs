namespace ExampleService
{
    using System.Threading.Tasks;
    using ExampleData;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Vanguard.Framework.Core.DomainEvents;

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

    /// <summary>
    /// The dummy event dispatcher.
    /// </summary>
    public class DummyEventDispatcher : IEventDispatcher
    {
        /// <inheritdoc />
        public void Dispatch(IDomainEvent domainEvent)
        {
            // do nothing
        }

        /// <inheritdoc />
        public Task DispatchAsync(IAsyncDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : IDomainEvent
        {
            // do nothing
        }

        /// <inheritdoc />
        public Task DispatchAsync<TEvent>(TEvent domainEvent)
            where TEvent : IAsyncDomainEvent
        {
            return Task.CompletedTask;
        }
    }
}
