namespace QuoteProviders
{
    public class StatusChangedEventArgs
    {
        private QuoteProviderStatus m_previous;
        private QuoteProviderStatus m_next;

        public StatusChangedEventArgs(QuoteProviderStatus previous, QuoteProviderStatus next)
        {
            m_previous = previous;
            m_next = next;
        }

        public QuoteProviderStatus Previous
        {
            get
            {
                return m_previous;
            }
        }

        public QuoteProviderStatus Next
        {
            get
            {
                return m_next;
            }
        }
    }
}
