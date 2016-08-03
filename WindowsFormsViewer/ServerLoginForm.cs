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
            if (m_username == null)
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            m_password = txtPassword.Text;
            if (m_password == null)
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
