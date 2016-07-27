using System;

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
        /// Initializes a new instance of the <c>ControlReplyMessage</c> class.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input byte array is not long enough.</exception>
        public ControlReplyMessage(byte[] message, int offset)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }

            if (message.Length - offset < HeaderLength)
            {
                throw new ArgumentOutOfRangeException();
            }

            m_bodyLength = message.ToInt32(offset + sizeof(int) + sizeof(ushort));
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
