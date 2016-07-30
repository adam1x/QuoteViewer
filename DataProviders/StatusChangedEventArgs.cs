namespace QuoteProviders
{
    /// <summary>
    /// Contains data for a status changed event.
    /// </summary>
    public class StatusChangedEventArgs
    {
        private QuoteProviderStatus m_old;
        private QuoteProviderStatus m_new;

        /// <summary>
        /// Initializes a new instance of the <c>StatusChangedEventArgs</c> class with the given previous and next status.
        /// </summary>
        /// <param name="old"></param>
        /// <param name="current"></param>
        public StatusChangedEventArgs(QuoteProviderStatus old, QuoteProviderStatus current)
        {
            m_old = old;
            m_new = current;
        }

        /// <summary>
        /// The previous status before change.
        /// </summary>
        public QuoteProviderStatus Old
        {
            get
            {
                return m_old;
            }
        }

        /// <summary>
        /// The next status after change.
        /// </summary>
        public QuoteProviderStatus New
        {
            get
            {
                return m_new;
            }
        }
    }
}
