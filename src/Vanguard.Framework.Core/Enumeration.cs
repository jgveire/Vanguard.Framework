namespace Vanguard.Framework.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The enumeration base class for handling enums in an alternative way.
    /// </summary>
    /// <seealso cref="System.IComparable" />
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="displayName">The display name.</param>
        protected Enumeration(int value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is equal to the right value.
        /// </returns>
        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return left?.Value == right?.Value;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is greater then the right value.
        /// </returns>
        public static bool operator >(Enumeration left, Enumeration right)
        {
            return (left?.Value ?? 0) > (right?.Value ?? 0);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is greater or equal to the right value.
        /// </returns>
        public static bool operator >=(Enumeration left, Enumeration right)
        {
            return (left?.Value ?? 0) >= (right?.Value ?? 0);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is not equal to the right value.
        /// </returns>
        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return left?.Value != right?.Value;
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is smaller then the right value.
        /// </returns>
        public static bool operator <(Enumeration left, Enumeration right)
        {
            return (left?.Value ?? 0) < (right?.Value ?? 0);
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>
        /// Returns <c>true</c> if the left value is smaller or equal then the right value.
        /// </returns>
        public static bool operator <=(Enumeration left, Enumeration right)
        {
            return (left?.Value ?? 0) <= (right?.Value ?? 0);
        }

        /// <summary>
        /// Gets the absolutes difference between two enumerations.
        /// </summary>
        /// <param name="firstValue">The first value.</param>
        /// <param name="secondValue">The second value.</param>
        /// <returns>The absolutes difference between two enumerations.</returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        /// <summary>
        /// Gets the enumeration from the display name.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
        /// <param name="displayName">The display name.</param>
        /// <returns>
        /// A enumeration.
        /// </returns>
        public static TEnumeration FromDisplayName<TEnumeration>(string displayName)
            where TEnumeration : Enumeration
        {
            var matchingItem = Parse<TEnumeration, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        /// <summary>
        /// Gets the enumeration from the value.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A enumeration.</returns>
        public static TEnumeration FromValue<TEnumeration>(int value)
            where TEnumeration : Enumeration
        {
            var matchingItem = Parse<TEnumeration, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        /// <summary>
        /// Gets all enumerations for the supplied type.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
        /// <returns>A collection of enumerations.</returns>
        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
            where TEnumeration : Enumeration
        {
            var type = typeof(TEnumeration);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                if (info.GetValue(null) is TEnumeration locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            return Value.CompareTo(((Enumeration)obj).Value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of the enumeration.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="description">The enumeration description.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A enumeration.</returns>
        /// <exception cref="ApplicationException">Thrown when the enumeration could not be found.</exception>
        private static TEnumeration Parse<TEnumeration, TValue>(TValue value, string description, Func<TEnumeration, bool> predicate)
            where TEnumeration : Enumeration
        {
            var matchingItem = GetAll<TEnumeration>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(TEnumeration));
                throw new InvalidOperationException(message);
            }

            return matchingItem;
        }
    }
}
