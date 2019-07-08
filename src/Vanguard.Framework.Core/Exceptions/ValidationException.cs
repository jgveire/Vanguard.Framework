namespace Vanguard.Framework.Core.Exceptions
{
    using System;

    /// <summary>
    /// The validation exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="target">The target of the validation exception. For example the member of a class.</param>
        public ValidationException(string message, string target)
            : base(message)
        {
            Target = target;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets the target of the validation exception.
        /// For example the member of a class.
        /// </summary>
        /// <value>
        /// The target of the validation exception.
        /// </value>
        public string? Target { get; }
    }
}
