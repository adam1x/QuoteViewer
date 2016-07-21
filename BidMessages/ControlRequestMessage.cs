namespace BidMessages
{
    /// <summary>
    /// Class <c>ControlReplyMessage</c> models all the <c>ControlMessage</c>s that are requests sent to the server.
    /// </summary>
    public abstract class ControlRequestMessage : ControlMessage
    {
		// [Xu Linqiu] 绝对不应出现这样的注释！ctor的实现是你的内部细节，你怎么做都行，但就是不能公开出来。
		/// <summary>
		/// This constructor doesn't do anything specific.
		/// </summary>
		public ControlRequestMessage()
        {
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes)
        {
            string body = GetBody();
            return (uint)textEncoding.GetBytes(body, 0, body.Length, bytes, HeaderLength);
        }

		// [Xu Linqiu] 此方法定义不当，缺乏弹性，目前只是Control request message的两个子类恰好它们的body都是一个string而已
		//             此现象只是巧合，不是protocol中的明确规定，未来当出现新的reply子类时，并不一定还是string值。
		/// <summary>
		/// This method returns the string value that is the body of a <c>ControlRequestMessage</c> object.
		/// </summary>
		/// <returns>A string value representing the body.</returns>
		protected abstract string GetBody();
    }
}
