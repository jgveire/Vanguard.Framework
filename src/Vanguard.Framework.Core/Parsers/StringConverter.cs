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
            else if (IsValidString(value))
            {
                return value.Substring(1, value.Length - 2);
            }

            throw new FormatException($"{value} is not a valid string value.");
        }

        private bool IsValidString(string value)
        {
            // Valid strings are:
            // ''
            // 'test'
            // 'test 123'
            return value.Length >= 2 &&
                   value[0] == '\'' &&
                   value[value.Length - 1] == '\'';
        }
    }
}
