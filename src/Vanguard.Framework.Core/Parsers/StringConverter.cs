namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The string converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class StringConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }
    }
}
