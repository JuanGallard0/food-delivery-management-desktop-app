using System;
using System.Windows.Forms;
using SourceCode.Controlador;
using SourceCode.Modelo;

namespace SourceCode.Vista
{
    public partial class FormChangePwd : Form
    {
        private string username;
        private string password;
        public FormChangePwd(string pUsername, string pPassword)
        {
            InitializeComponent();
            username = pUsername;
            password = pPassword;
        }

        private void FormChangePwd_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Updating " + username + "'s password.";
        }
        
        private void btnPwdChange_Click(object sender, EventArgs e)
        {
            bool actualIgual = Encryptor.CompararMD5(txtOld.Text, password);
            bool nuevaIgual = txtNew.Text.Equals(txtConfirm.Text);
            bool nuevaValida = txtNew.Text.Length > 0;

            if (actualIgual && nuevaIgual && nuevaValida)
            {
                try
                {
                    AppUserDAO.passwordChange(username, Encryptor.CrearMD5(txtNew.Text));

                    MessageBox.Show("Password updated successfully!",
                        "HUGO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error, try again please.",
                        "HUGO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Error, incorrect input.",
                    "HUGO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void txtPasswordChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPwdChange_Click(sender, e);
        }
    }
}