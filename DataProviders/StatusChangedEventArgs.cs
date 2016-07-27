namespace QuoteProviders
{
    /// <summary>
    /// Contains data for a status changed event.
    /// </summary>
    public class StatusChangedEventArgs
    {
        private QuoteProviderStatus m_previous;
        private QuoteProviderStatus m_next;

        /// <summary>
        /// Initializes a new instance of the <c>StatusChangedEventArgs</c> class with the given previous and next status.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public StatusChangedEventArgs(QuoteProviderStatus previous, QuoteProviderStatus next)
        {
            m_previous = previous;
            m_next = next;
        }

        /// <summary>
        /// The previous status before change.
        /// </summary>
        public QuoteProviderStatus Previous
        {
            get
            {
                return m_previous;
            }
        }

        /// <summary>
        /// The next status after change.
        /// </summary>
        public QuoteProviderStatus Next
        {
            get
            {
                return m_next;
            }
        }
    }
}
