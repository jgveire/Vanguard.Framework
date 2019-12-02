namespace Vanguard.Framework.Core.Models
{
    using System;

    /// <summary>
    /// The unique model.
    /// </summary>
    public class UniqueModel : IUniqueModel
    {
        /// <summary>
        /// Gets or sets the model identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
