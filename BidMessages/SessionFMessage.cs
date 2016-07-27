using System;

namespace BidMessages
{
    /// <summary>
    /// Models <c>QuoteMessage</c>s of Session F.
    /// </summary>
    public class SessionFMessage : SessionCEFHMessage
    {
        /// <summary>
        /// Initializes a new instance of the <c>SessionFMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionFMessage</c>.</param>
        /// <param name="offset">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session F message.</exception>
        public SessionFMessage(byte[] message, int offset, int count)
            : base(message, offset, count)
        {
            if (PeekSession(message, offset) != AuctionSession)
            {
                throw new ArgumentException("Session mismatch.");
            }
        }

        /// <summary>
        /// The message's auction session.
        /// </summary>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionF;
            }
        }
    }
}
