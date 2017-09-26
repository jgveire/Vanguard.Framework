using System;

namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <seealso cref="IDataEntity" />
    public interface IUniqueEntity : IUniqueEntity<Guid>
    {
    }
}
