using System;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Models the server's response to a <c>LoginRequestMessage</c>.
    /// </summary>
    public class LoginReplyMessage : ControlReplyMessage
    {
        private int m_maxHeartbeatInterval;

        /// <summary>
        ///Initializes a new instance of the <c>LoginReply</c> class with its corresponding byte array.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input offset is out of range.</exception>
        /// <exception cref="System.ArgumentException">The input byte array does not represent a login reply message.</exception>
        public LoginReplyMessage(byte[] message, int offset)
            : base(message, offset)
        {
            if (message == null || message.Length <= 0)
            {
                throw new ArgumentNullException("message cannot be null or empty.");
            }

            if (offset < 0 || message.Length - offset < HeaderLength + sizeof(int))
            {
                throw new ArgumentOutOfRangeException("offset out of range.");
            }

            if (PeekFunctionCode(message, offset) != Function)
            {
                throw new ArgumentException("Function code mismatch.");
            }

            m_maxHeartbeatInterval = message.ToInt32(offset + HeaderLength);
            Debug.Assert(m_maxHeartbeatInterval >= 0);
        }

        /// <summary>
        /// The message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginReply;
            }
        }

        /// <summary>
        /// The max heartbeat interval sent in login reply.
        /// </summary>
        public int MaxHeartbeatInterval
        {
            get { return m_maxHeartbeatInterval; }
        }

        /// <summary>
        /// Encodes the body of a <c>LoginReplyMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] bytes, int offset)
        {
            int body = m_maxHeartbeatInterval;
            return body.GetBytes(bytes, offset);
        }

        /// <summary>
        /// Gets a string representation for the specified <c>LoginReplyMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and the max heartbeat interval.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, m_maxHeartbeatInterval);
        }
    }
}
