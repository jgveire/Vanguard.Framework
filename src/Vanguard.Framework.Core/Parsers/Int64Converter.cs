namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The integer 64 bit converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class Int64Converter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Convert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (long.TryParse(value, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid Int64 value.");
        }
    }
}
