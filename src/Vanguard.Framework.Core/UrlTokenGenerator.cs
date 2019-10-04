namespace Vanguard.Framework.Core
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// The URL token generator.
    /// It generates a token that can be used in URL's to valid
    /// for example email addresses, password resets, etc.
    /// </summary>
    public class UrlTokenGenerator : IUrlTokenGenerator
    {
        private static readonly RNGCryptoServiceProvider CryptoProvider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Gets the default URL token generator instance.
        /// </summary>
        public static IUrlTokenGenerator Instance { get; } = new UrlTokenGenerator();

        /// <summary>
        /// Generates an URL token according to the specified length.
        /// </summary>
        /// <param name="length">The length of the token.</param>
        /// <returns>An URL token.</returns>
        public string Generate(int length)
        {
            Guard.ArgumentInRange(length, 1, int.MaxValue, nameof(length));

            // Make sure the length is dividable by 3
            // otherwise we will get padding characters.
            int byteLength = length - (length % 3) + 3;
            var bytes = new byte[byteLength];

            CryptoProvider.GetBytes(bytes);
            
            // Convert to string and replace padding characters.
            string token = Convert
                .ToBase64String(bytes)
                .Replace('/', 'a')
                .Replace('=', 'B')
                .Replace('+', 'c');

            return token.Substring(0, length);
        }
    }
}
