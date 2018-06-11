namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The integer 32 converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class Int32Converter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (int.TryParse(value, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid Int32 value.");
        }
    }
}
