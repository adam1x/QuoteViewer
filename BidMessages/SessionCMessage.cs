using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session C.
    /// </summary>
    public class SessionCMessage : SessionCEFHMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionCMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionCMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.Exception">Input byte array does not represent a session C message.</exception>
        public SessionCMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
            if (PeekSession(message, startIndex) != AuctionSession)
            {
                throw new Exception("Session mismatch.");
            }
        }

        /// <value>
        /// Property <c>AuctionSession</c> represents the message's auction session.
        /// </value>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionC;
            }
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>SessionCMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and update time.</returns>
        public override string ToString()
        {
            return "Session C: " + UpdateTimestamp;
        }
    }
}
