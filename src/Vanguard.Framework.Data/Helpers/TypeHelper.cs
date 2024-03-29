﻿namespace Vanguard.Framework.Data.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type helper class.
    /// </summary>
    public static class TypeHelper
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
            typeof(decimal),
        };

        /// <summary>
        /// Determines whether the specified type is a byte array.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is a byte array; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsByteArray(Type type)
        {
            return type == typeof(byte[]) ||
                   type == typeof(byte?[]);
        }

        /// <summary>
        /// Determines whether the specified type is boolean.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is boolean; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBoolean(Type type)
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
        public static bool IsDateTime(Type type)
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
        public static bool IsNumeric(Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            return NumericTypes.Contains(type) ||
                   (nullableType != null && NumericTypes.Contains(nullableType));
        }
    }
}
