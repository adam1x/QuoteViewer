namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写
	// [Xu Linqiu] summary注释文字错误
	/// <summary>
	/// Class <c>SessionAMsg</c> models <c>QuoteMessage</c>s of Session D.
	/// </summary>
	public class SessionDMsg : SessionDGMsg
    {
		// [Xu Linqiu] summary注释内容不当
		/// <summary>
		/// This constructor calls the base class <c>QuoteDataMessage</c>'s constructor.
		/// </summary>
		/// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
		/// <param name="startIndex">the starting index to read the body.</param>
		/// <param name="count">the number of bytes to read in <c>body</c>.</param>
		public SessionDMsg(byte[] body, int startIndex, int count)
            : base(body, startIndex, count)
        {
        }
    }
}
