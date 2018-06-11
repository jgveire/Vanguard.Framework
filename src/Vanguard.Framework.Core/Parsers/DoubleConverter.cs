namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The integer 16 bit converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class DoubleConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (double.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid double value.");
        }
    }
}
