namespace Vanguard.Framework.Core.Parsers
{
    /// <summary>
    /// The string converter.
    /// </summary>
    /// <seealso cref="TypeConverterBase" />
    internal class StringConverter : TypeConverterBase
    {
        /// <inheritdoc />
        public override object Convert(string value)
        {
            Guard.ArgumentNotNull(value, nameof(value));
            if (IsNullValue(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }
    }
}
