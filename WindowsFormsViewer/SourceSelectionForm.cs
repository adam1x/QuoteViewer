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
        private string m_filePath;
        private string m_serverAddress;
        private int m_port;

        public SourceSelectionForm()
        {
            InitializeComponent();
            m_filePath = null;
            m_serverAddress = null;
            m_port = -1;
        }

        internal string FilePath
        {
            get
            {
                return m_filePath;
            }
        }

        internal string ServerAddress
        {
            get
            {
                return m_serverAddress;
            }
        }

        internal int Port
        {
            get
            {
                return m_port;
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            openFileDialog1.Filter = "DAT files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_filePath = openFileDialog1.FileName;
                Close();
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

            Close();
        }
    }
}
