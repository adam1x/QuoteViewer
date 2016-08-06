using System;
using System.Windows.Forms;

namespace FormViewer
{
    public partial class ServerLoginForm : Form
    {
        private string m_username;
        private string m_password;

        public ServerLoginForm()
        {
            InitializeComponent();
            m_username = null;
            m_password = null;
        }

        internal string Username
        {
            get
            {
                return m_username;
            }
        }

        internal string Password
        {
            get
            {
                return m_password;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            m_username = txtUsername.Text;
            m_password = txtPassword.Text;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
