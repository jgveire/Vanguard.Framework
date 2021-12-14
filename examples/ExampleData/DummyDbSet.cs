namespace ExampleData
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    /// The dummy database set.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal class DummyDbSet<TEntity> : DbSet<TEntity>
        where TEntity : class
    {
        public override IEntityType EntityType => throw new NotImplementedException();
    }
}
