using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using QuoteProviders;

namespace WindowsFormsViewer
{
    internal class Manager
    {
        public Manager()
        {

        }

        public void Start(DataViewerForm viewerForm)
        {
            if (viewerForm == null)
            {
                throw new ArgumentNullException("viewerForm cannot be null.");
            }

            SourceSelectionForm sourceForm = new SourceSelectionForm();
            sourceForm.ShowDialog();

            if (sourceForm.FilePath != null)
            {
                viewerForm.Provider = new FileQuoteProvider(sourceForm.FilePath);
            }
            else if (sourceForm.ServerAddress != null && sourceForm.Port != -1)
            {
                ServerLoginForm loginForm = new ServerLoginForm();
                loginForm.ShowDialog();

                Debug.Assert(loginForm.Username != null && loginForm.Password != null);

                viewerForm.Provider = new TcpQuoteProvider(sourceForm.ServerAddress, sourceForm.Port,
                                                           loginForm.Username, loginForm.Password);
            }
            else
            {
                Debug.Assert(false);
            }

            viewerForm.Start();
        }
    }
}
