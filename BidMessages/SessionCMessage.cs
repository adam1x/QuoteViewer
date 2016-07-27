using System;

namespace BidMessages
{
    /// <summary>
    /// Models <c>QuoteMessage</c>s of Session C.
    /// </summary>
    public class SessionCMessage : SessionCEFHMessage
    {
        /// <summary>
        /// Initializes a new instance of the <c>SessionCMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionCMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session C message.</exception>
        public SessionCMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
            if (PeekSession(message, startIndex) != AuctionSession)
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
                return AuctionSessions.SessionC;
            }
        }
    }
}
