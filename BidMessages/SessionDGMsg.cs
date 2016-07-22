using System.Collections.Generic;

namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写
	// [Xu Linqiu] summary注释文字错误
	/// <summary>
	/// Class <c>SessionAMsg</c> models <c>QuoteMessage</c>s of Sessions D and G.
	/// </summary>
	public class SessionDGMsg : QuoteTextMessage
    {
		// [Xu Linqiu] 应为private
		protected static Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
        {
            { QuoteFieldTags.UpdateTimestamp, 0 },
            { QuoteFieldTags.AuctionSession, 1 },
            { QuoteFieldTags.ContentText, 2 },
            { QuoteFieldTags.ProcessedCount, 3 },
            { QuoteFieldTags.PendingCount, 4 },
        };

		// [Xu Linqiu] summary注释内容不当
		/// <summary>
		/// This constructor calls the base class <c>QuoteDataMessage</c>'s constructor.
		/// </summary>
		/// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
		/// <param name="startIndex">the starting index to read the body.</param>
		/// <param name="count">the number of bytes to read in <c>body</c>.</param>
		public SessionDGMsg(byte[] body, int startIndex, int count)
            : base(body, startIndex, count)
        {
        }

		// [Xu Linqiu] 应提供此类消息所包含quote field对应的Property

		// [Xu Linqiu] 注释里不应暴露实现细节 - m_body
		/// <summary>
		/// This method generates a dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.
		/// </summary>
		/// <returns>A dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.</returns>
		protected override Dictionary<QuoteFieldTags, int> GetTagToIndexMap()
        {
            return m_tagToIndex;
        }
    }
}
