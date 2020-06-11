using System;

namespace AisCodeChallenge.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur during application execution
    /// </summary>
    [Serializable]
    public class AisCodeChallengeException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public AisCodeChallengeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AisCodeChallengeException(string message)
                : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="messageFormat">The exception message format.</param>
        /// <param name="args">The exception message arguments.</param>
        public AisCodeChallengeException(string messageFormat, params object[] args)
                : base(string.Format(messageFormat, args))
        {
        }
    }
}
