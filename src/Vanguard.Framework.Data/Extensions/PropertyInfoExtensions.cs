namespace Vanguard.Framework.Data.Extensions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The type extensions.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the type is a string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <c>true</c> if the type is a string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateTime(this Type source)
        {
            return source == typeof(DateTime);
        }

        /// <summary>
        /// Determines whether the type is a string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <c>true</c> if the type is a string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsString(this Type source)
        {
            return source == typeof(string);
        }
    }
}
