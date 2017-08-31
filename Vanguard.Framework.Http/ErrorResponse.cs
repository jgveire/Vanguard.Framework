namespace Vanguard.Framework.Http
{
    /// <summary>
    /// The error response class.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        public ErrorResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public ErrorResponse(string code, string message)
        {
            Error = new Error(code, message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="target">The error target. For example the field that is causing the error.</param>
        public ErrorResponse(string code, string message, string target)
        {
            Error = new Error(code, message, target);
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public Error Error { get; set; }
    }
}
