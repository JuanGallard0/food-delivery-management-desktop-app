using System;
using System.Windows.Forms;
using SourceCode.Controlador;
using SourceCode.Modelo;

namespace SourceCode.Vista
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            pupulateControls();
        } 
        
        private void pupulateControls()
        {
              cmbUsername.DataSource = null;
              cmbUsername.ValueMember = "password";
              cmbUsername.DisplayMember = "username";
              cmbUsername.DataSource = AppUserDAO.getList();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Encryptor.CompararMD5(txtPassword.Text, cmbUsername.SelectedValue.ToString()))
            {
                AppUser u = (AppUser) cmbUsername.SelectedItem;

                MessageBox.Show("¡Welcome!",
                    "HUGO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (u.userType)
                {
                    FormMain ventana = new FormMain(u);
                    ventana.Owner = ((Form) this.TopLevelControl);
                    this.Hide();
                    ventana.ShowDialog();
                    this.Close();
                }
                else
                {
                    FormUsuario ventana = new FormUsuario(u);
                    ventana.Owner = this;
                    this.Hide();
                    ventana.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Wrong password!", "HUGO", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);
        }


        private void btnPwdChange_Click(object sender, EventArgs e)
        {
            FormChangePwd unaVentana = new FormChangePwd(cmbUsername.Text, cmbUsername.SelectedValue.ToString());
            unaVentana.ShowDialog();
            pupulateControls();
        }
    }
}