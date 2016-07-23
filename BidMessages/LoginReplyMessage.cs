using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>LoginReplyMessage</c> models the server's response to a <c>LoginRequestMessage</c>.
    /// </summary>
    public class LoginReplyMessage : ControlReplyMessage
    {
        private uint m_maxHeartbeatInterval;

        /// <summary>
        /// This constructor initializes a new instance of the <c>LoginReply</c> class with its corresponding byte array.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <exception cref="System.Exception">The input byte array does not represent a login reply message.</exception>
        public LoginReplyMessage(byte[] message)
        {
            if (PeekFunctionCode(message) != Function)
            {
                throw new Exception("Function code mismatch.");
            }
            m_maxHeartbeatInterval = message.ToUInt32(HeaderLength);
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginReply;
            }
        }

        /// <value>
        /// Property <c>MaxHeartbeatInterval</c> represents the max heartbeat interval sent in login reply.
        /// </value>
        public uint MaxHeartbeatInterval
        {
            get { return m_maxHeartbeatInterval; }
        }

        /// <summary>
        /// This method encodes the body of a <c>LoginReplyMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes, int offset)
        {
            uint body = m_maxHeartbeatInterval;
            body.GetBytes(bytes, offset);
            return sizeof(uint);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>LoginReplyMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and the max heartbeat interval.</returns>
        public override string ToString()
        {
            return "Login reply: " + m_maxHeartbeatInterval;
        }
    }
}
