using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>LoginReplyMessage</c> models the server's response to a <c>LoginRequestMessage</c>.
    /// </summary>
    public class LoginReplyMessage : ControlReplyMessage
    {
        private int m_maxHeartbeatInterval;

        /// <summary>
        /// This constructor initializes a new instance of the <c>LoginReply</c> class with its corresponding byte array.
        /// </summary>
        /// <param name="message">the byte array representation of this message.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input byte array is not long enough.</exception>
        /// <exception cref="System.ArgumentException">The input byte array does not represent a login reply message.</exception>
        public LoginReplyMessage(byte[] message, int offset)
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

            m_maxHeartbeatInterval = message.ToInt32(offset + HeaderLength);
        }

        /// <summary>
        /// Property <c>Function</c> represents the message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginReply;
            }
        }

        /// <summary>
        /// Property <c>MaxHeartbeatInterval</c> represents the max heartbeat interval sent in login reply.
        /// </summary>
        public int MaxHeartbeatInterval
        {
            get { return m_maxHeartbeatInterval; }
        }

        /// <summary>
        /// This method encodes the body of a <c>LoginReplyMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] bytes, int offset)
        {
            int body = m_maxHeartbeatInterval;
            body.GetBytes(bytes, offset);
            return sizeof(int);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>LoginReplyMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and the max heartbeat interval.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, m_maxHeartbeatInterval);
        }
    }
}
