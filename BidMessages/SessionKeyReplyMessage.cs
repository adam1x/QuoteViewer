using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionKeyReplyMessage</c> models the server's response to a <c>SessionKeyRequestMessage</c>.
    /// </summary>
    public class SessionKeyReplyMessage : ControlReplyMessage
    {
        private uint m_sessionKey;

        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionKeyReplyMessage</c> class with the given byte array.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <exception cref="System.Exception">The input byte array does not represent a session key reply message.</exception>
        public SessionKeyReplyMessage(byte[] message)
        {
            if (PeekFunctionCode(message) != Function)
            {
                throw new Exception("Function code mismatch.");
            }
            m_sessionKey = message.ToUInt32(HeaderLength);
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.SessionKeyReply;
            }
        }

        /// <value>
        /// Property <c>SessionKey</c> represents the message's content: session key.
        /// </value>
        public uint SessionKey
        {
            get { return m_sessionKey; }
        }

        /// <summary>
        /// This method encodes the body of a <c>SessionKeyReplyMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes, int offset)
        {
            uint body = m_sessionKey;
            body.GetBytes(bytes, offset);
            return sizeof(uint);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>SessionKeyReplyMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and session key.</returns>
        public override string ToString()
        {
            return "Session key reply: " + m_sessionKey;
        }
    }
}
