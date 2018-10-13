namespace Vanguard.Framework.Core.Parsers
{
    using System;

    /// <summary>
    /// The GUID converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class GuidConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Convert(string value)
        {
            Guard.ArgumentNotNullOrEmpty(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else if (Guid.TryParse(value, out var result))
            {
                return result;
            }

            throw new FormatException($"{value} is not a valid GUID value.");
        }
    }
}
