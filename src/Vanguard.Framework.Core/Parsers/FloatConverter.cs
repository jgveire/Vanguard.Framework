namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The float converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class FloatConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Covert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (float.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid float value.");
        }
    }
}
