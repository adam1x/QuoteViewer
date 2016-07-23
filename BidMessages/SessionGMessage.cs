using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session G.
    /// </summary>
    public class SessionGMessage : SessionDGMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionGMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionGMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.Exception">Input byte array does not represent a session G message.</exception>
        public SessionGMessage(byte[] message, int startIndex, int count)
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
                return AuctionSessions.SessionG;
            }
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>SessionGMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and update time.</returns>
        public override string ToString()
        {
            return "Session G: " + UpdateTimestamp;
        }
    }
}
