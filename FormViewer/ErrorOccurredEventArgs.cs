using System;

namespace FormViewer
{
    public class ErrorOccurredEventArgs
    {
        private Exception m_error;
        private bool m_severe;

        public ErrorOccurredEventArgs(Exception ex, bool severe)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex cannot be null.");
            }

            m_error = ex;
            m_severe = severe;
        }

        public Exception Error
        {
            get
            {
                return m_error;
            }
        }

        public bool IsSevere
        {
            get
            {
                return m_severe;
            }
        }
    }
}
