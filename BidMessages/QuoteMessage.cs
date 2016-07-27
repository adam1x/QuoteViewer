using System;
using System.Collections.Generic;
using System.Globalization;

namespace BidMessages
{
    /// <summary>
    /// Models the messages that contains quote information.
    /// </summary>
    public abstract class QuoteMessage : BidMessage
    {
        private static readonly int[] AuctionSessionPriority = new int[]
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

        private int m_bodyLength;

        /// <summary>
        /// All the fields a <c>QuoteMessage</c> object has in strings.
        /// </summary>
        private string[] m_fields;

        /// <summary>
        /// Initializes a new instance of the <c>QuoteMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>QuoteMessage</c>.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The input byte array is not long enough.</exception>
        public QuoteMessage(byte[] message, int offset, int count)
        {
            if (message == null)
            {
                throw new ArgumentNullException();
            }

            if (message.Length - offset < count)
            {
                throw new ArgumentOutOfRangeException();
            }

            m_bodyLength = count - HeaderLength;
            string bodyString = TextEncoding.GetString(message, offset + HeaderLength, m_bodyLength);
            m_fields = bodyString.Split(',');
        }

        /// <summary>
        /// The message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.Quote;
            }
        }

        /// <summary>
        /// The message's update timestamp.
        /// </summary>
        public DateTime UpdateTimestamp
        {
            get
            {
                return GetFieldValueAsDateTime(GetIndexFromTag(QuoteFieldTags.UpdateTimestamp));
            }
        }

        /// <summary>
        /// The message's auction session.
        /// </summary>
        public abstract AuctionSessions AuctionSession { get; }

        /// <summary>
        /// The message's fields.
        /// </summary>
        public string this[int index]
        {
            get
            {
                return GetFieldValueAsString(index);
            }
        }

        #region Get value from string fields
        /// <summary>
        /// Gets the Int32 value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The int value associated with the tag or <c>defaultValue</c>.</returns>
        public int GetFieldValueAsInt32(QuoteFieldTags tag, int defaultValue)
        {
            return GetFieldValueAsInt32(GetIndexFromTag(tag), defaultValue);
        }

        /// <summary>
        /// Gets the Int32 value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <returns>The int value associated with the tag or 0.</returns>
        public int GetFieldValueAsInt32(QuoteFieldTags tag)
        {
            return GetFieldValueAsInt32(tag, 0);
        }

        /// <summary>
        /// Tries to convert a string in this message's fields to an int value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The int value parsed from the field or <c>defaultValue</c>.</returns>
        /// <exception cref="System.FormatException">The field cannot be parsed as an Int32 value.</exception>
        public int GetFieldValueAsInt32(int index, int defaultValue = 0)
        {
            string raw;
            raw = GetFieldValueAsString(index);
            if (string.IsNullOrEmpty(raw))
            {
                return defaultValue;
            }

            int result;
            if (int.TryParse(raw, out result))
            {
                return result;
            }

            throw new FormatException();
        }

        /// <summary>
        /// Gets the DateTime value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The DateTime value associated with the tag or <c>defaultValue</c>.</returns>
        public DateTime GetFieldValueAsDateTime(QuoteFieldTags tag, DateTime defaultValue)
        {
            return GetFieldValueAsDateTime(GetIndexFromTag(tag), defaultValue);
        }

        /// <summary>
        /// Gets the DateTime value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <returns>The DateTime value associated with the tag or <c>DateTime.MinValue</c>.</returns>
        public DateTime GetFieldValueAsDateTime(QuoteFieldTags tag)
        {
            return GetFieldValueAsDateTime(tag, DateTime.MinValue);
        }

        /// <summary>
        /// Tries to convert a string in this message's fields to a DateTime value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The DateTime value parsed from the field or <c>defaultValue</c>.</returns>
        /// <exception cref="System.FormatException">The field cannot be parsed as an DateTime value.</exception>
        public DateTime GetFieldValueAsDateTime(int index, DateTime defaultValue)
        {
            string raw;
            raw = GetFieldValueAsString(index);
            if (string.IsNullOrEmpty(raw))
            {
                return defaultValue;
            }

            DateTime result;
            if (DateTime.TryParseExact(GetFieldValueAsString(index), "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            throw new FormatException();
        }

        /// <summary>
        /// Tries to converts a string in this message's fields to a DateTime value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <returns>The DateTime value parsed from the field or <c>DateTime.MinValue</c>.</returns>
        public DateTime GetFieldValueAsDateTime(int index)
        {
            return GetFieldValueAsDateTime(index, DateTime.MinValue);
        }

        /// <summary>
        /// Gets the TimeSpan value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The TimeSpan value associated with the tag or <c>defaultValue</c>.</returns>
        public TimeSpan GetFieldValueAsTimeSpan(QuoteFieldTags tag, TimeSpan defaultValue)
        {
            return GetFieldValueAsTimeSpan(GetIndexFromTag(tag), defaultValue);
        }

        /// <summary>
        /// Gets the TimeSpan value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <returns>The TimeSpan value associated with the tag or <c>TimeSpan.Zero</c>.</returns>
        public TimeSpan GetFieldValueAsTimeSpan(QuoteFieldTags tag)
        {
            return GetFieldValueAsTimeSpan(tag, TimeSpan.Zero);
        }

        /// <summary>
        /// Tries to convert a string in this message's fields to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <param name="defaultValue">the default value to return if the field doesn't exist or the retrieved string is empty.</param>
        /// <returns>The TimeSpan value parsed from the field or <c>defaultValue</c>.</returns>
        /// <exception cref="System.FormatException">The field cannot be parsed as an TimeSpan value.</exception>
        public TimeSpan GetFieldValueAsTimeSpan(int index, TimeSpan defaultValue)
        {
            string raw;
            raw = GetFieldValueAsString(index);
            if (string.IsNullOrEmpty(raw))
            {
                return defaultValue;
            }

            TimeSpan result;
            if (TimeSpan.TryParseExact(GetFieldValueAsString(index), "g", CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// Tries to convert a string in this message's fields to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <returns>The TimeSpan value parsed from the field or <c>TimeSpan.Zero</c>.</returns>
        public TimeSpan GetFieldValueAsTimeSpan(int index)
        {
            return GetFieldValueAsTimeSpan(index, TimeSpan.Zero);
        }

        /// <summary>
        /// Extracts an <c>AuctionSessions</c> value out of this message's fields.
        /// </summary>
        /// <returns>The <c>AuctionSessions</c> value parsed from the field.</returns>
        public AuctionSessions GetFieldValueAsAuctionSessions()
        {
            return (AuctionSessions)GetFieldValueAsString(GetIndexFromTag(QuoteFieldTags.AuctionSession))[0];
        }

        /// <summary>
        /// Gets the string value associated with the specified tag.
        /// </summary>
        /// <param name="tag">a tag representing a field of this message.</param>
        /// <returns>The string value associated with the tag.</returns>
        public string GetFieldValueAsString(QuoteFieldTags tag)
        {
            return GetFieldValueAsString(GetIndexFromTag(tag));
        }

        /// <summary>
        /// Tries to retrieve a string in this message's fields.
        /// </summary>
        /// <param name="index">the index in fields.</param>
        /// <returns>The string in the field.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Out of range array access.</exception>
        public string GetFieldValueAsString(int index)
        {
            if (index < 0 || index >= m_fields.Length)
            {
                return null;
            }
            return m_fields[index];
        }

        /// <summary>
        /// Gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array or -1 if the field doesn't exist.</returns>
        public abstract int GetIndexFromTag(QuoteFieldTags tag);
        #endregion

        /// <summary>
        /// Retrieves the <c>AuctionSessoins</c> value from a <c>QuoteMessage</c>'s byte array representaion.
        /// </summary>
        /// <param name="message">the byte array representing a <c>QuoteMessage</c>.</param>
        /// <param name="startIndex">the start index.</param>
        /// <returns>The session of this message.</returns>
        /// <exception cref="System.FormatException">The input byte array is malformed.</exception>
        /// <exception cref="System.NotSupportedException">The session is unsupported or malformed message.</exception>
        public static AuctionSessions PeekSession(byte[] message, int startIndex)
        {
            const int offset = 25; // offset from start to session
            if (message[startIndex + offset - 1] != (byte)',' || message[startIndex + offset + 1] != (byte)',')
            {
            	throw new FormatException();
            }

            switch (message[startIndex + offset])
            {
                case (byte)'A':
                    return AuctionSessions.SessionA;

                case (byte)'B':
                    return AuctionSessions.SessionB;

                case (byte)'C':
                    return AuctionSessions.SessionC;

                case (byte)'D':
                    return AuctionSessions.SessionD;

                case (byte)'E':
                    return AuctionSessions.SessionE;

                case (byte)'F':
                    return AuctionSessions.SessionF;

                case (byte)'G':
                    return AuctionSessions.SessionG;

                case (byte)'H':
                    return AuctionSessions.SessionH;

                default:
                    throw new NotSupportedException("Unsuppoted message session.");
            }
        }

        /// <summary>
        /// Encodes the body of a <c>QuoteMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] target, int offset)
        {
            string body = string.Join(",", m_fields);
            return TextEncoding.GetBytes(body, 0, body.Length, target, offset);
        }

        /// <summary>
        /// Gets the length of the body of this message.
        /// </summary>
        /// <returns>The length of the body of this message.</returns>
        protected override int GetBodyLength()
        {
            return m_bodyLength;
        }

        /// <summary>
        /// Gets a string representation for the specified <c>SessionBMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and update time.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, UpdateTimestamp);
        }

        #region Message Comparison
        /// <summary>
        /// Compares two <c>QuoteMessage</c> objects.
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
        /// Compares two <c>AuctionSessions</c> values.
        /// </summary>
        /// <param name="s1">the first <c>AuctionSessions</c> value.</param>
        /// <param name="s2">the second <c>AuctionSessions</c> value.</param>
        /// <returns>A value greater than 0 if s1 > s2, 0 if s1 == s2, and a value smaller than 1 if s1 &lt; s2.</returns>
        public static int CompareSession(AuctionSessions s1, AuctionSessions s2)
        {
            return (AuctionSessionPriority[s1 - AuctionSessions.SessionA] - AuctionSessionPriority[s2 - AuctionSessions.SessionA]);
        }

        /// <summary>
        /// Determines whether a given object is equal to the current <c>QuoteMessage</c> object.
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
        /// Gets the hash code of the current object.
        /// </summary>
        /// <returns>The hash code of the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Defines a short hand to check whether one <c>QuoteMessage</c> object is greater than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 > m2, false otherwise.</returns>
        public static bool operator >(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) > 0);
        }

        /// <summary>
        /// Defines a short hand to check whether one <c>QuoteMessage</c> object is smaller than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 &lt; m2, false otherwise.</returns>
        public static bool operator <(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) < 0);
        }

        /// <summary>
        /// Defines a short hand to check whether two <c>QuoteMessage</c> objects are equal.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 == m2, false otherwise.</returns>
        public static bool operator ==(QuoteMessage m1, QuoteMessage m2)
        {
            return (Compare(m1, m2) == 0);
        }

        /// <summary>
        /// Defines a short hand to check whether two <c>QuoteMessage</c> objects are not equal.
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
