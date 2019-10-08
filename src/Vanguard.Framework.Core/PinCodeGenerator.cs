namespace Vanguard.Framework.Core
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// The pin code generator.
    /// </summary>
    public class PinCodeGenerator : IPinCodeGenerator
    {
        private static readonly RNGCryptoServiceProvider CryptoProvider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Gets the default pin generator instance.
        /// </summary>
        public static IPinCodeGenerator Instance { get; } = new PinCodeGenerator();

        /// <summary>
        /// Generates a pin code according to the specified length.
        /// The minimum supported length is one and the maximum supported length 20.
        /// </summary>
        /// <param name="length">The length of the pin code.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length isn't between and 20.</exception>
        /// <returns>A pin code.</returns>
        public string Generate(int length)
        {
            Guard.ArgumentInRange(length, 1, 20, nameof(length));

            var bytes = new byte[sizeof(ulong)];
            CryptoProvider.GetBytes(bytes);
            var pin = Math.Abs(BitConverter.ToInt64(bytes, 0));

            return pin.ToString().PadRight(20, '0').Substring(0, length);
        }
    }
}
