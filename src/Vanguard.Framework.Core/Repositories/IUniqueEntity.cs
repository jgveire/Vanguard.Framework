namespace Vanguard.Framework.Core.Repositories
{
    using System;

    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <seealso cref="IDataEntity" />
    public interface IUniqueEntity : IUniqueEntity<Guid>
    {
    }
}
