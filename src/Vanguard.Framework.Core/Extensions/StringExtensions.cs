namespace Vanguard.Framework.Core.Extensions
{
    using System;

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>
        /// <c>true</c> if the value parameter occurs within this string, or if value is the empty string (""); otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null || value == null)
            {
                return false;
            }

            return source.IndexOf(value, comparisonType) != -1;
        }

        /// <summary>
        /// Gets the next character in the string relative to the specified index
        /// of null when the end of the string has been reached.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <returns>The next character in the string relative to the specified index.</returns>
        internal static char? Next(this string source, int index)
        {
            int nextIndex = index + 1;
            if (source != null && nextIndex < source.Length)
            {
                return source[nextIndex];
            }

            return null;
        }

        /// <summary>
        /// Gets the previous character in the string relative to the specified index
        /// or null when the specified index is lower then one.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <returns>The precious character in the string relative to the specified index.</returns>
        internal static char? Previous(this string source, int index)
        {
            int prevIndex = index - 1;
            if (source != null && prevIndex >= 0)
            {
                return source[prevIndex];
            }

            return null;
        }
    }
}
