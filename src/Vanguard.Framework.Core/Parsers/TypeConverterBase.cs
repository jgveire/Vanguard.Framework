namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The type converter base class.
    /// </summary>
    internal abstract class TypeConverterBase
    {
        /// <summary>
        /// Coverts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        public abstract object Covert(string value);

        /// <summary>
        /// Determines whether the value is a null value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if value is a null value; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsNullValue(string value)
        {
            Guard.ArgumentNotNull(value, nameof(value));
            return value.Equals("null", StringComparison.OrdinalIgnoreCase);
        }
    }
}
