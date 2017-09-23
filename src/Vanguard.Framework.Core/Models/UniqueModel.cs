using System;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Core.Models
{
    /// <summary>
    /// The unique model class.
    /// </summary>
    public class UniqueModel : IUniqueEntity
    {
        /// <summary>
        /// Gets or sets the model identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
