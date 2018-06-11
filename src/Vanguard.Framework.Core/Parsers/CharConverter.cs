namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The char converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class CharConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (value.Length == 3)
            {
                return value[1];
            }

            throw new FormatException($"{value} is not a valid char value.");
        }
    }
}
