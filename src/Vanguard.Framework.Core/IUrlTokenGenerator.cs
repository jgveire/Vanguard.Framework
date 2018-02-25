namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The URL token generator.
    /// It generates a token that can be used in URL's to valid
    /// for example email addresses, password resets, etc.
    /// </summary>
    public interface IUrlTokenGenerator
    {
        /// <summary>
        /// Generates an URL token according to the specified length.
        /// </summary>
        /// <param name="length">The length of the token.</param>
        /// <returns>An URL token.</returns>
        string Generate(int length);
    }
}