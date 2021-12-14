namespace Vanguard.Framework.Data.Builders
{
    using System;
    using System.Globalization;
    using System.Text;
    using Vanguard.Framework.Data.Helpers;

    /// <summary>
    /// The JSON builder.
    /// </summary>
    internal class JsonBuilder
    {
        private readonly CultureInfo _cultureInfo = new CultureInfo("en-US");
        private readonly StringBuilder stringBuilder = new StringBuilder();

        /// <summary>
        /// Adds a JSON property.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <param name="value">The value.</param>
        public void AddProperty(string name, Type valueType, object? value)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(",");
            }

            var stringValue = ConvertValue(valueType, value);
            stringBuilder.Append($"\"{name}\":{stringValue}");
        }

        /// <summary>
        /// Clears JSON builder.
        /// </summary>
        public void Clear()
        {
            stringBuilder.Clear();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{{stringBuilder}}}";
        }

        private string? ConvertValue(Type valueType, object? value)
        {
            if (value == null)
            {
                return "null";
            }

            var stringValue = Convert.ToString(value, _cultureInfo);
            if (TypeHelper.IsNumeric(valueType))
            {
                return stringValue;
            }
            else if (TypeHelper.IsBoolean(valueType))
            {
                return stringValue?.ToLower();
            }
            else if (TypeHelper.IsDateTime(valueType))
            {
                return string.Format("\"{0:O}\"", value);
            }

            return $"\"{stringValue}\"";
        }
    }
}
