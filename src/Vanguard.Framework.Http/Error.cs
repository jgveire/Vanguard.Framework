using System.Collections.Generic;

namespace Vanguard.Framework.Http
{
    /// <summary>
    /// The error class.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="target">The error target. For example the field that is causing the error.</param>
        public Error(string code, string message, string target)
        {
            Code = code;
            Message = message;
            Target = target;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error target.
        /// For example the field that is causing the error.
        /// </summary>
        /// <value>
        /// The error target.
        /// </value>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        /// <value>
        /// The error details.
        /// </value>
        public IEnumerable<Error> Details { get; set; }
    }
}
