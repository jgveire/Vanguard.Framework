namespace Vanguard.Framework.Core
{
    /// <summary>
    /// The current user interface.
    /// </summary>
    /// <remarks>
    /// Please make sure that a user identifier is always returned.
    /// For anonymous users you could for example a GUID
    /// or the string "Anonymous".
    /// </remarks>
    public interface ICurrentUser
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string UserId { get; }
    }
}
