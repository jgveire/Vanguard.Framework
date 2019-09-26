namespace Vanguard.Framework.Data.Tests.Repositories
{
    using Microsoft.EntityFrameworkCore;

    public class DummyDbSet<TEntity> : DbSet<TEntity>
        where TEntity : class
    {
    }
}
