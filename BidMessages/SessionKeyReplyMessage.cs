using System;

namespace BidMessages
{
    /// <summary>
    /// Models the server's response to a <c>SessionKeyRequestMessage</c>.
    /// </summary>
    public class SessionKeyReplyMessage : ControlReplyMessage
    {
        private uint m_sessionKey;

        /// <summary>
        /// Initializes a new instance of the <c>SessionKeyReplyMessage</c> class with the given byte array.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input byte array is not long enough.</exception>
        /// <exception cref="System.ArgumentException">The input byte array does not represent a session key reply message.</exception>
        public SessionKeyReplyMessage(byte[] message, int offset)
            : base(message, offset)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }

            if (message.Length - offset < HeaderLength + sizeof(int))
            {
                throw new ArgumentOutOfRangeException();
            }

            if (PeekFunctionCode(message, offset) != Function)
            {
                throw new ArgumentException("Function code mismatch.");
            }
            m_sessionKey = message.ToUInt32(offset + HeaderLength);
        }

        /// <summary>
        /// The message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.SessionKeyReply;
            }
        }

        /// <summary>
        /// The message's content: session key.
        /// </summary>
        public uint SessionKey
        {
            get { return m_sessionKey; }
        }

        /// <summary>
        /// Encodes the body of a <c>SessionKeyReplyMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] target, int offset)
        {
            uint body = m_sessionKey;
            body.GetBytes(target, offset);
            return sizeof(int);
        }

        /// <summary>
        /// Gets a string representation for the specified <c>SessionKeyReplyMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and session key.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, m_sessionKey);
        }
    }
}
