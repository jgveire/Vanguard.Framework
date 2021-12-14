namespace Vanguard.Framework.Data.Tests.Repositories
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class DummyDbSet<TEntity> : DbSet<TEntity>
        where TEntity : class
    {
        public override IEntityType EntityType => throw new NotImplementedException();
    }
}
