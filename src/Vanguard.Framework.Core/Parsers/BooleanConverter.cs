namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The boolean converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class BooleanConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Convert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                value.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (value.Equals("false", StringComparison.OrdinalIgnoreCase) ||
                     value.Equals("0", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            throw new FormatException($"{value} is not a valid boolean value.");
        }
    }
}
