using System;

namespace Vanguard.Framework.Core.Repositories
{
    /// <summary>
    /// The unique entity interface.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Core.Repositories.IEntity" />
    public interface IUniqueEntity : IUniqueEntity<Guid>
    {
    }
}
