namespace BidMessages
{
    /// <summary>
    /// Class <c>ControlMessage</c> models messages sent and received during connecting to and authencating on the remote server.
    /// </summary>
    public abstract class ControlMessage : BidMessage
    {
		// [Xu Linqiu] 绝对不应出现这样的注释！ctor的实现是你的内部细节，你怎么做都行，但就是不能公开出来。
		/// <summary>
		/// This contructor doesn't do anything specific.
		/// </summary>
		public ControlMessage()
        {
        }
    }
}
