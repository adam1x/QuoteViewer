using System;
using System.Collections.Generic;
using System.Globalization;

namespace BidMessage
{
    /// <summary>
    /// Class <c>QuoteMessage</c> models the messages that contains quote information.
    /// </summary>
    public abstract class QuoteMessage : BidMessage
    {
        protected string[] m_body;

        /// <summary>
        /// This constructor initializes the new <c>QuoteMessage</c> to contain an array of strings representing the various quote information.
        /// </summary>
        /// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index to read the body.</param>
        /// <param name="count">the number of bytes to read in <c>body</c>.</param>
        public QuoteMessage(byte[] body, int startIndex, int count)
        {
            string bodyString = textEncoding.GetString(body, startIndex, count);
            m_body = bodyString.Split(',');
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override ushort Function
        {
            get
            {
                return (ushort)Messages.Quote;
            }
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public string this[int index]
        {
            get { return m_body[index]; }
        }

        /// <summary>
        /// This method generates a dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.
        /// </summary>
        /// <returns>A dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.</returns>
        protected abstract Dictionary<QuoteFieldTags, int> GetTagToIndexMap();

        /// <summary>
        /// This method gets the index to the string array <c>m_body</c> given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in <c>m_body</c> to get a string encoding of a field.</returns>
        public int GetIndexFromTag(QuoteFieldTags tag)
        {
            int result;
            if (GetTagToIndexMap().TryGetValue(tag, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        #region Read value from byte[]
        /// <summary>
        /// This method tries to convert a string found at <c>m_body[index]</c> to an int value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent an int value.</param>
        /// <returns>The int value parsed from <c>m_body[index]</c> or <c>defaultVal</c> if parsing fails.</returns>
        public int GetIntValue(int index, int defaultVal = 0)
        {
            int result;
            if (int.TryParse(this[index], out result))
            {
                return result;
            }
            else
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// This method tries to convert a string found at <c>m_body[index]</c> to a uint value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a uint value.</param>
        /// <returns>The uint value parsed from <c>m_body[index]</c> or <c>defaultVal</c> if parsing fails.</returns>
        public uint GetUIntValue(int index, uint defaultVal = 0)
        {
            uint result;
            if (uint.TryParse(this[index], out result))
            {
                return result;
            }
            else
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// This method tries to convert a string found at <c>m_body[index]</c> to a DateTime value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a DateTime value.</param>
        /// <returns>The DateTime value parsed from <c>m_body[index]</c> or <c>defaultVal</c> if parsing fails.</returns>
        public DateTime GetDateTimeValue(int index, DateTime defaultVal)
        {
            DateTime result;
            if (DateTime.TryParseExact(this[index], "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// This method overloads the method: <c>public DateTime GetDateTimeValue(int index, DateTime defaultVal)</c>.
        /// It tries to convert a string found at <c>m_body[index]</c> to a DateTime value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a DateTime value.</param>
        /// <returns>The DateTime value parsed from <c>m_body[index]</c> or <c>DateTime.MinValue</c> if parsing fails.</returns>
        public DateTime GetDateTimeValue(int index)
        {
            return GetDateTimeValue(index, DateTime.MinValue);
        }

        /// <summary>
        /// This method tries to convert a string found at <c>m_body[index]</c> to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a TimeSpan value.</param>
        /// <returns>The TimeSpan value parsed from <c>m_body[index]</c> or <c>defaultVal</c> if parsing fails.</returns>
        public TimeSpan GetTimeSpanValue(int index, TimeSpan defaultVal)
        {
            TimeSpan result;
            if (TimeSpan.TryParseExact(this[index], "g", CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            else
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// This method overloads the method: <c>public TimeSpan GetTimeSpanValue(int index, TimeSpan defaultVal)</c>.
        /// It tries to convert a string found at <c>m_body[index]</c> to a TimeSpan value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a TimeSpan value.</param>
        /// <returns>The TimeSpan value parsed from <c>m_body[index]</c> or <c>TimeSpan.Zero</c> if parsing fails.</returns>
        public TimeSpan GetTimeSpanValue(int index)
        {
            return GetTimeSpanValue(index, TimeSpan.Zero);
        }

        /// <summary>
        /// This method tries to convert a string found at <c>m_body[index]</c> to a string value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a string value.</param>
        /// <returns>The string value parsed from <c>m_body[index]</c> or <c>defaultVal</c> if parsing fails.</returns>
        public string GetStringValue(int index, string defaultVal)
        {
            if (index < 0 || index >= m_body.Length)
            {
                return defaultVal;
            }
            return this[index];
        }

        /// <summary>
        /// This method overloads the method: <c>public string GetStringValue(int index, string defaultVal)</c>.
        /// It tries to convert a string found at <c>m_body[index]</c> to a string value.
        /// </summary>
        /// <param name="index">the index in <c>m_body</c>.</param>
        /// <param name="defaultVal">the default value to return if the retrieved string does not represent a string value.</param>
        /// <returns>The string value parsed from <c>m_body[index]</c> or <c>string.Empty</c> if parsing fails.</returns>
        public string GetStringValue(int index)
        {
            return GetStringValue(index, string.Empty);
        }

        /// <summary>
        /// This method extracts an <c>AuctionSessions</c> value out of <c>m_body</c>.
        /// </summary>
        /// <returns>The <c>AuctionSessions</c> value extracted from <c>m_body</c>.</returns>
        public AuctionSessions GetAuctionSessionsValue()
        {
            return (AuctionSessions)this[GetIndexFromTag(QuoteFieldTags.AuctionSession)][0];
        }
        #endregion

        /// <summary>
        /// This method reetrieves the <c>AuctionSessoins</c> value from a <c>QuoteMessage</c>'s byte array representaion.
        /// </summary>
        /// <param name="body">the byte array representing a <c>QuoteMessage</c>.</param>
        /// <param name="startIndex">the starting index in <c>body</c> for the quote message.</param>
        /// <returns>The session this <c>QuoteMessage</c> object.</returns>
        public static AuctionSessions PeekSession(byte[] body, int startIndex)
        {
            string s = textEncoding.GetString(body, startIndex + 14, 3);
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
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes)
        {
            string body = string.Join(",", m_body);
            return (uint)textEncoding.GetBytes(body, 0, body.Length, bytes, HeaderLength);
        }

        #region Message Comparison
        /// <summary>
        /// This method compares two <c>QuoteMessage</c>s.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c>.</param>
        /// <param name="m2">the first <c>QuoteMessage</c>.</param>
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

            DateTime updateTime1 = m1.GetDateTimeValue(0);
            DateTime updateTime2 = m2.GetDateTimeValue(0);
            int result = DateTime.Compare(updateTime1, updateTime2);
            if (result > 0)
            {
                return 1;
            }
            else if (result < 0)
            {
                return -1;
            }

            AuctionSessions session1 = m1.GetAuctionSessionsValue();
            AuctionSessions session2 = m2.GetAuctionSessionsValue();
            int sessionComparison = CompareSession(session1, session2);
            if (sessionComparison != 0)
            {
                return sessionComparison;
            }

            if (m1 is QuoteDataMessage)
            {
                TimeSpan serverTime1 = m1.GetTimeSpanValue(13);
                TimeSpan serverTime2 = m2.GetTimeSpanValue(13);
                result = TimeSpan.Compare(serverTime1, serverTime2);
                if (result > 0)
                {
                    return 1;
                }
                else if (result < 0)
                {
                    return -1;
                }

                if (m1 is SessionAMsg)
                {
                    int bidQuantity1 = m1.GetIntValue(14);
                    int bidQuantity2 = m2.GetIntValue(14);
                    if (bidQuantity1 > bidQuantity2)
                    {
                        return 1;
                    }
                    else if (bidQuantity1 < bidQuantity2)
                    {
                        return -1;
                    }
                }

                int bidPrice1 = m1.GetIntValue(15);
                int bidPrice2 = m2.GetIntValue(14);
                if (bidPrice1 > bidPrice2)
                {
                    return 1;
                }
                else if (bidPrice1 < bidPrice2)
                {
                    return -1;
                }

                DateTime bidTime1 = m1.GetDateTimeValue(16);
                DateTime bidTime2 = m2.GetDateTimeValue(15);
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
            Dictionary<AuctionSessions, int> dict = new Dictionary<AuctionSessions, int>
            {
                { AuctionSessions.SessionC, 0 },
                { AuctionSessions.SessionA, 1 },
                { AuctionSessions.SessionB, 2 },
                { AuctionSessions.SessionG, 3 },
                { AuctionSessions.SessionD, 4 },
                { AuctionSessions.SessionE, 4 },
                { AuctionSessions.SessionF, 4 },
                { AuctionSessions.SessionH, 4 },
            };

            return dict[s1] - dict[s2];
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

            return Compare(this, msg) == 0;
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
        /// This method defines a short hand to check whether one <c>QuoteMessage</c> is greater than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 > m2, false otherwise.</returns>
        public static bool operator >(QuoteMessage m1, QuoteMessage m2)
        {
            return Compare(m1, m2) > 0;
        }

        /// <summary>
        /// This method defines a short hand to check whether one <c>QuoteMessage</c> is smaller than another.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 &lt; m2, false otherwise.</returns>
        public static bool operator <(QuoteMessage m1, QuoteMessage m2)
        {
            return Compare(m1, m2) < 0;
        }

        /// <summary>
        /// This method defines a short hand to check whether two <c>QuoteMessage</c>s are equal.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 == m2, false otherwise.</returns>
        public static bool operator ==(QuoteMessage m1, QuoteMessage m2)
        {
            return Compare(m1, m2) == 0;
        }

        /// <summary>
        /// This method defines a short hand to check whether two <c>QuoteMessage</c>s are not equal.
        /// </summary>
        /// <param name="m1">the first <c>QuoteMessage</c> object.</param>
        /// <param name="m2">the second <c>QuoteMessage</c> object.</param>
        /// <returns>True if m1 != m2, false otherwise.</returns>
        public static bool operator !=(QuoteMessage m1, QuoteMessage m2)
        {
            return Compare(m1, m2) != 0;
        }
        #endregion
    }
}
