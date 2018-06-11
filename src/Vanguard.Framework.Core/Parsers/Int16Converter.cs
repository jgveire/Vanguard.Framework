namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The integer 16 bit converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class Int16Converter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (short.TryParse(value, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid Int16 value.");
        }
    }
}
