using System;

namespace Warcraft.NET.Exceptions
{
    /// <summary>
    /// This exception thrown when an chunk signature not found in data stream
    /// is expected to be in valid RIFF format.
    /// </summary>
    public class ChunkSignatureNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkSignatureNotFoundException"/> class, along with a specified
        /// message.
        /// </summary>
        /// <param name="message">The message included in the exception.</param>
        public ChunkSignatureNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkSignatureNotFoundException"/> class, along with a specified
        /// message and inner exception which caused this exception.
        /// </summary>
        /// <param name="message">The message included in the exception.</param>
        /// <param name="innerException">The exception which caused this exception.</param>
        public ChunkSignatureNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
