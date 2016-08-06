using System;
using System.Diagnostics;

using QuoteProviders;

namespace WindowsFormsViewer
{
    internal class FormsManager
    {
        public static readonly FormsManager UniqueInstance = new FormsManager();

        private FormsManager()
        {
        }

        public IQuoteDataProvider GetProvider()
        {
            SourceSelectionForm sourceForm = new SourceSelectionForm();

            while (true)
            {
                sourceForm.ShowDialog();

                IQuoteDataProvider provider = null;

                if (sourceForm.FilePath != null)
                {
                    try
                    {
                        provider = new FileQuoteProvider(sourceForm.FilePath);
                    }
                    catch (Exception ex)
                    {
                        sourceForm.OnErrorOccurred(ex);
                        continue;
                    }
                }
                else if (sourceForm.ServerAddress != null && sourceForm.Port != -1)
                {
                    ServerLoginForm loginForm = new ServerLoginForm();
                    loginForm.ShowDialog();

                    if (loginForm.Username == null && loginForm.Password == null)
                    {
                        sourceForm.ResetServerAddress();
                        continue;
                    }

                    try
                    {
                        provider = new TcpQuoteProvider(sourceForm.ServerAddress, sourceForm.Port,
                                                        loginForm.Username, loginForm.Password);
                    }
                    catch (Exception ex)
                    {
                        sourceForm.OnErrorOccurred(ex);
                        continue;
                    }
                }

                return provider;
            }
        }
    }
}
