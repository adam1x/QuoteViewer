using System;
using System.Collections.Generic;
using System.Globalization;

namespace BidMessages
{
    /// <summary>
    /// Class <c>QuoteMessage</c> models the messages that contains quote information.
    /// </summary>
    public abstract class QuoteMessage : BidMessage
    {
        private static readonly int[] AuctionSessionsComparison = new int[]
        {
            1, // sessoin A
            2, // sessoin B
            0, // sessoin C
            4, // sessoin D
            4, // sessoin E
            4, // sessoin F
            3, // sessoin G
            4, // sessoin H
        };

        /// <summary>
        /// This instance variable represents all the fields a <c>QuoteMessage</c> object has in strings.
        /// </summary>
        private string[] m_fields;

        /// <summary>
        /// This constructor initializes a new instance of the <c>QuoteMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>QuoteMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        public QuoteMessage(byte[] message, int startIndex, int count)
        {
            string bodyString = TextEncoding.GetString(message, startIndex + HeaderLength, count - HeaderLength);
            m_fields = bodyString.Split(',');
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.Quote;
            }
        }

        /// <value>
        /// Property <c>UpdateTimestamp</c> represents the message's update timestamp.
        /// </value>
        public DateTime UpdateTimestamp
        {
            get
            {
                return GetFieldValueAsDateTime(GetIndexFromTag(QuoteFieldTags.UpdateTimestamp));
            }
        }

        /// <value>
        /// Property <c>AuctionSession</c> represents the message's auction session.
        /// </value>
        public abstract AuctionSessions AuctionSession { get; }

        /// <value>
        /// This indexor represents the message's fields.
        /// </value>
        public string this[int index]
        {
            get
            {
                return GetFieldValueAsString(index);
            }
        }

        /// <summary>
        /// This method generates a dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices to the message's fields.
        /// </summary>
        /// <returns>A dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in the fields.</returns>
        protected abstract Dictionary<QuoteFieldTags, int> GetTagToIndexMap();

        /// <summary>
        /// This method gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array to get a string encoding of a field.</returns>
        public int GetIndexFromTag(QuoteFieldTags tag)
        {
            int result;
            if (GetTagToIndexMap().TryGetValue(tag, out result))
            {
                return result;
            }
            return -1;
        }

        #region Get value from string fields
        /// <summary>
        /// This method tries to convert a string in this message's fields to an int value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent an int value.</param>
        /// <returns>The int value parsed from the field or <c>defaultValue</c> if parsing fails.</returns>
        public int GetFieldValueAsInt32(int index, int defaultValue = 0)
        {
            int result;
            if (int.TryParse(GetFieldValueAsString(index), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// This method tries to convert a string in this message's fields to a uint value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent a uint value.</param>
        /// <returns>The uint value parsed from the field or <c>defaultValue</c> if parsing fails.</returns>
        public uint GetFieldValueAsUInt32(int index, uint defaultValue = 0)
        {
            uint result;
            if (uint.TryParse(GetFieldValueAsString(index), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// This method tries to convert a string in this message's fields to a DateTime value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent a DateTime value.</param>
        /// <returns>The DateTime value parsed from the field or <c>defaultValue</c> if parsing fails.</returns>
        public DateTime GetFieldValueAsDateTime(int index, DateTime defaultValue)
        {
            DateTime result;
            if (DateTime.TryParseExact(GetFieldValueAsString(index), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// This method overloads the method: <c>public DateTime GetDateTimeValue(int index, DateTime defaultValue)</c>.
        /// It tries to convert a string in this message's fields to a DateTime value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent a DateTime value.</param>
        /// <returns>The DateTime value parsed from the field or <c>DateTime.MinValue</c> if parsing fails.</returns>
        public DateTime GetFieldValueAsDateTime(int index)
        {
            return GetFieldValueAsDateTime(index, DateTime.MinValue);
        }

        /// <summary>
        /// This method tries to convert a string in this message's fields to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent a TimeSpan value.</param>
        /// <returns>The TimeSpan value parsed from the field or <c>defaultValue</c> if parsing fails.</returns>
        public TimeSpan GetFieldValueAsTimeSpan(int index, TimeSpan defaultValue)
        {
            TimeSpan result;
            if (TimeSpan.TryParseExact(GetFieldValueAsString(index), "g", CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// This method overloads the method: <c>public TimeSpan GetTimeSpanValue(int index, TimeSpan defaultValue)</c>.
        /// It tries to convert a string in this message's fields to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the retrieved string does not represent a TimeSpan value.</param>
        /// <returns>The TimeSpan value parsed from the field or <c>TimeSpan.Zero</c> if parsing fails.</returns>
        public TimeSpan GetFieldValueAsTimeSpan(int index)
        {
            return GetFieldValueAsTimeSpan(index, TimeSpan.Zero);
        }

        /// <summary>
        /// This method overloads the method: <c>public string GetStringValue(int index, string defaultValue)</c>.
        /// It tries to convert a string in this message's fields to a string value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <returns>The string value found in the field.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Out of range array access.</exception>
        public string GetFieldValueAsString(int index)
        {
            if (index < 0 || index >= m_fields.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return m_fields[index];
        }

        /// <summary>
        /// This method extracts an <c>AuctionSessions</c> value out of this message's fields.
        /// </summary>
        /// <returns>The <c>AuctionSessions</c> value parsed from the field.</returns>
        public AuctionSessions GetFieldValueAsAuctionSessions()
        {
            return (AuctionSessions)this[GetIndexFromTag(QuoteFieldTags.AuctionSession)][0];
        }
        #endregion

        /// <summary>
        /// This method retrieves the <c>AuctionSessoins</c> value from a <c>QuoteMessage</c>'s byte array representaion.
        /// </summary>
        /// <param name="message">the byte array representing a <c>QuoteMessage</c>.</param>
        /// <param name="startIndex">the start index.</param>
        /// <exception cref="System.NotSupportedException">The session is unsupported or malformed message.</exception>
        /// <returns>The session of this message.</returns>
        public static AuctionSessions PeekSession(byte[] message, int startIndex)
        {
            int offset = 24; // offset from start to comma before session
            string s = TextEncoding.GetString(message, startIndex + offset, 3);
            switch (s)
            {
                case ",A,":
                    return AuctionSessions.SessionA;
                case ",B,":
                    return AuctionSessions.SessionB;
                case ",C,":
                    return AuctionSessions.SessionC;
                case ",D,":
                    return AuctionSessions.SessionD;
                case ",E,":
                    return AuctionSessions.SessionE;
                case ",F,":
                    return AuctionSessions.SessionF;
                case ",G,":
                    return AuctionSessions.SessionG;
                case ",H,":
                    return AuctionSessions.SessionH;
                default:
                    throw new NotSupportedException("Unsuppoted message session.");
            }
        }

        /// <summary>
        /// This method encodes the body of a <c>QuoteMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes, int offset)
        {
            string body = string.Join(",", m_fields);
            return (uint)TextEncoding.GetBytes(body, 0, body.Length, bytes, offset);
        }

        #region Message Comparison
        /// <summary>
        /// This method compares two <c>QuoteMessage</c> objects.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the first <c>QuoteMessage</c> object.</param>
        /// <returns>1 if m1 > m2, 0 if m1 == m2, and -1 if m1 &lt; m2.</returns>
        public static int Compare(QuoteMessage m1, QuoteMessage m2)
        {
            if ((object)m1 == null)
            {
                if ((object)m2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else if ((object)m2 == null)
            {
                return 1;
            }

            DateTime updateTime1 = m1.GetFieldValueAsDateTime(0);
            DateTime updateTime2 = m2.GetFieldValueAsDateTime(0);
            int result = DateTime.Compare(updateTime1, updateTime2);
            if (result > 0)
            {
                return 1;
            }
            else if (result < 0)
            {
                return -1;
            }

            AuctionSessions session1 = m1.GetFieldValueAsAuctionSessions();
            AuctionSessions session2 = m2.GetFieldValueAsAuctionSessions();
            int sessionComparison = CompareSession(session1, session2);
            if (sessionComparison != 0)
            {
                return sessionComparison;
            }

            if (m1 is QuoteDataMessage)
            {
                TimeSpan serverTime1 = m1.GetFieldValueAsTimeSpan(13);
                TimeSpan serverTime2 = m2.GetFieldValueAsTimeSpan(13);
                result = TimeSpan.Compare(serverTime1, serverTime2);
                if (result > 0)
                {
                    return 1;
                }
                else if (result < 0)
                {
                    return -1;
                }

                if (m1 is SessionAMessage)
                {
                    int bidQuantity1 = m1.GetFieldValueAsInt32(14);
                    int bidQuantity2 = m2.GetFieldValueAsInt32(14);
                    if (bidQuantity1 > bidQuantity2)
                    {
                        return 1;
                    }
                    else if (bidQuantity1 < bidQuantity2)
                    {
                        return -1;
                    }
                }

                int bidPrice1 = m1.GetFieldValueAsInt32(15);
                int bidPrice2 = m2.GetFieldValueAsInt32(14);
                if (bidPrice1 > bidPrice2)
                {
                    return 1;
                }
                else if (bidPrice1 < bidPrice2)
                {
                    return -1;
                }

                DateTime bidTime1 = m1.GetFieldValueAsDateTime(16);
                DateTime bidTime2 = m2.GetFieldValueAsDateTime(15);
                result = DateTime.Compare(bidTime1, bidTime2);
                if (result < 0)
                {
                    return 1;
                }
                else if (result > 0)
                {
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// This method compares two <c>AuctionSessions</c> values.
        /// </summary>
        /// <param name="s1">the first <c>AuctionSessions</c> value.</param>
        /// <param name="s2">the second <c>AuctionSessions</c> value.</param>
        /// <returns>A value greater than 0 if s1 > s2, 0 if s1 == s2, and a value smaller than 1 if s1 &lt; s2.</returns>
        public static int CompareSession(AuctionSessions s1, AuctionSessions s2)
        {
            return (AuctionSessionsComparison[s1 - AuctionSessions.SessionA] - AuctionSessionsComparison[s2 - AuctionSessions.SessionA]);
        }

        /// <summary>
        /// This method determines whether a given object is equal to the current <c>QuoteMessage</c> object.
        /// </summary>
        /// <param name="obj">the object for comparison.</param>
        /// <returns>True if <c>obj</c> is a <c>QuoteMessage</c> object and is equal to the current object; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            QuoteMessage msg = obj as QuoteMessage;
            if ((object)msg == null)
            {
                return false;
            }

            return (Compare(this, msg) == 0);
        }

        /// <summary>
        /// This method gets the hash code of the current object.
        /// </summary>
        /// <returns>The hash code of the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// This method defines a short hand to check whether one <c>QuoteMessage</c> object is greater than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 > m2, false otherwise.</returns>
        public static bool operator >(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) > 0);
        }

        /// <summary>
        /// This method defines a short hand to check whether one <c>QuoteMessage</c> object is smaller than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 &lt; m2, false otherwise.</returns>
        public static bool operator <(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) < 0);
        }

        /// <summary>
        /// This method defines a short hand to check whether two <c>QuoteMessage</c> objects are equal.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 == m2, false otherwise.</returns>
        public static bool operator ==(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) == 0);
        }

        /// <summary>
        /// This method defines a short hand to check whether two <c>QuoteMessage</c> objects are not equal.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 != m2, false otherwise.</returns>
        public static bool operator !=(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) != 0);
        }
        #endregion
    }
}
