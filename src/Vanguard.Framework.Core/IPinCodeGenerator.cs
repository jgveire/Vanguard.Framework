namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The pin code generator.
    /// </summary>
    public interface IPinCodeGenerator
    {
        /// <summary>
        /// Generates a pin code according to the specified length.
        /// </summary>
        /// <param name="length">The length of the pin code.</param>
        /// <returns>A pin code.</returns>
        string Generate(int length);
    }
}