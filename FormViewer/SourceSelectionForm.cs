using System;
using System.Net;
using System.Windows.Forms;

namespace FormViewer
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

        private void SourceSelectionForm_Load(object sender, EventArgs e)
        {
            numServerPort.Minimum = IPEndPoint.MinPort;
            numServerPort.Maximum = IPEndPoint.MaxPort;
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

                ResetServerAddress();

                Close();
            }
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            m_serverAddress = txtServerAddress.Text;
            if (string.IsNullOrEmpty(m_serverAddress))
            {
                MessageBox.Show("Please enter a server address.", "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal portInput = numServerPort.Value;
            if (portInput % 1 != 0)
            {
                MessageBox.Show("Please enter a whole number for port.", "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numServerPort.Value = 0;
                return;
            }

            m_port = (int)portInput;

            ResetFilePath();

            Close();
        }

        internal void ResetFilePath()
        {
            m_filePath = null;
        }

        internal void ResetServerAddress()
        {
            m_serverAddress = null;
            m_port = -1;
            txtServerAddress.ResetText();
            numServerPort.Value = 0;
        }

        internal void OnErrorOccurred(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
