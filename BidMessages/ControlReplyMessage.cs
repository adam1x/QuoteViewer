using System;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Models all the <c>ControlMessage</c>s that are replies from the server.
    /// </summary>
    public abstract class ControlReplyMessage : ControlMessage
    {
        /// <summary>
        /// The length of this message's body.
        /// </summary>
        protected int m_bodyLength;

        /// <summary>
        /// Initializes a new instance of the <c>ControlReplyMessage</c> class with the given byte array and an offset.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null or empty.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input offset is out of range.</exception>
        public ControlReplyMessage(byte[] message, int offset)
        {
            if (message == null || message.Length <= 0)
            {
                throw new ArgumentNullException("message cannot be null or empty.");
            }

            if (offset < 0 || message.Length - offset < MinLength)
            {
                throw new ArgumentOutOfRangeException("offset out of range.");
            }

            m_bodyLength = message.ToInt32(offset + sizeof(int) + sizeof(ushort));
            Debug.Assert(m_bodyLength > 0);
        }

        /// <summary>
        /// Gets the length of the body of this message.
        /// </summary>
        /// <returns>The length of the body of this message.</returns>
        protected override int GetBodyLength()
        {
            return m_bodyLength;
        }
    }
}
