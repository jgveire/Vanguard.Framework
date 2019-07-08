namespace ExampleData
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The dummy database set.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal class DummyDbSet<TEntity> : DbSet<TEntity>
        where TEntity : class
    {
    }
}
