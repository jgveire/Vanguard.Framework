namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The decimal converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class DecimalConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object? Convert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (decimal.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid decimal value.");
        }
    }
}
