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
    public partial class ServerLoginForm : Form
    {
        private SourceSelectionForm m_sourceForm;

        public ServerLoginForm(SourceSelectionForm sourceForm)
        {
            if (sourceForm == null)
            {
                throw new ArgumentNullException("sourceForm cannot be null");
            }

            InitializeComponent();
            m_sourceForm = sourceForm;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            if (username == null)
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            string password = txtPassword.Text;
            if (password == null)
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            m_sourceForm.SetCredentials(username, password);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
