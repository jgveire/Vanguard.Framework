namespace Vanguard.Framework.Core.Models
{
    using System;
    using Vanguard.Framework.Core.Repositories;

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
