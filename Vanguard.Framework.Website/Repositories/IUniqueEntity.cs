using System;

namespace Vanguard.Framework.Website.Repositories
{
    public interface IUniqueEntity
    {
        Guid Id { get; set; }
    }
}
