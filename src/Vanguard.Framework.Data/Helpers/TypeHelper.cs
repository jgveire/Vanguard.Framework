using System;
using System.Collections.Generic;

namespace Vanguard.Framework.Data.Helpers
{
    /// <summary>
    /// The type helper class.
    /// </summary>
    internal static class TypeHelper
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(IntPtr),
            typeof(UIntPtr),
            typeof(char),
            typeof(double),
            typeof(float),
            typeof(float),
            typeof(decimal)
        };

        /// <summary>
        /// Determines whether the specified type is boolean.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is boolean; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsBoolean(Type type)
        {
            return type == typeof(bool) ||
                   type == typeof(bool?);
        }

        /// <summary>
        /// Determines whether the specified type is DateTime.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is DateTime; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsDateTime(Type type)
        {
            return type == typeof(DateTime) ||
                   type == typeof(DateTime?);
        }

        /// <summary>
        /// Determines whether the specified type is numeric.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is numeric; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsNumeric(Type type)
        {
            return NumericTypes.Contains(type) ||
                   NumericTypes.Contains(Nullable.GetUnderlyingType(type));
        }
    }
}
