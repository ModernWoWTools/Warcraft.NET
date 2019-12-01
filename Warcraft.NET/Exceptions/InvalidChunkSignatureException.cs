using System;

namespace Warcraft.NET.Exceptions
{
    /// <summary>
    /// This exception thrown when an invalid or unknown chunk signature is found during parsing of binary data which
    /// is expected to be in valid RIFF format.
    /// </summary>
    public class InvalidChunkSignatureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidChunkSignatureException"/> class, along with a specified
        /// message.
        /// </summary>
        /// <param name="message">The message included in the exception.</param>
        public InvalidChunkSignatureException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidChunkSignatureException"/> class, along with a specified
        /// message and inner exception which caused this exception.
        /// </summary>
        /// <param name="message">The message included in the exception.</param>
        /// <param name="innerException">The exception which caused this exception.</param>
        public InvalidChunkSignatureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
