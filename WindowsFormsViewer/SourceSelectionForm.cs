using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsViewer
{
    public partial class SourceSelectionForm : Form
    {
        private DataViewerForm m_mainForm;
        private string m_filePath;
        private string m_serverAddress;
        private int m_port;
        private string m_username;
        private string m_password;

        public SourceSelectionForm(DataViewerForm mainForm)
        {
            if (mainForm == null)
            {
                throw new ArgumentNullException("mainForm cannot be null.");
            }

            InitializeComponent();
            m_mainForm = mainForm;
            m_filePath = null;
            m_serverAddress = null;
            m_port = 0;
            m_username = null;
            m_password = null;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            openFileDialog1.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_filePath = openFileDialog1.FileName;
                m_mainForm.SetQuoteDataProvider(m_filePath);
                OnValidInput();
            }
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            m_serverAddress = txtServerAddress.Text;
            if (string.IsNullOrEmpty(m_serverAddress))
            {
                MessageBox.Show("Please enter a server address.");
                return;
            }

            string serverPort = txtServerPort.Text;
            if (string.IsNullOrEmpty(serverPort) || 
                (!string.IsNullOrEmpty(serverPort) &&
                 !int.TryParse(serverPort, out m_port)) )
            {
                MessageBox.Show("Please enter a number for port.");
                return;
            }

            ServerLoginForm loginForm = new ServerLoginForm(this);
            loginForm.ShowDialog();
        }

        internal void SetCredentials(string username, string password)
        {
            if (username == null || password == null)
            {
                throw new ArgumentNullException("username and password cannot be null.");
            }

            m_username = username;
            m_password = password;

            m_mainForm.SetQuoteDataProvider(m_serverAddress, m_port, m_username, m_password);
            OnValidInput();
        }

        internal void OnValidInput()
        {
            m_mainForm.Run();
            Close();
        }
    }
}
