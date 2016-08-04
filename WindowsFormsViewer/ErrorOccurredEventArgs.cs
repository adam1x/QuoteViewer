using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsViewer
{
    public class ErrorOccurredEventArgs
    {
        private Exception m_ex;
        private bool m_severe;

        public ErrorOccurredEventArgs(Exception ex, bool severe)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex cannot be null.");
            }

            m_ex = ex;
            m_severe = severe;
        }

        public Exception Ex
        {
            get
            {
                return m_ex;
            }
        }

        public bool Severe
        {
            get
            {
                return m_severe;
            }
        }
    }
}
