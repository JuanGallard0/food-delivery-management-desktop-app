using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SourceCode.Modelo;


namespace SourceCode.Vista
{
    public partial class FormUsuario : Form
    {
        private AppUser user;
        

        public FormUsuario(AppUser pUser)
        {
            InitializeComponent();
            user = pUser;
            DoubleBuffered = true;
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            lblWelcome.Text =
                "Welcome " + user.username + " [" + (user.userType ? "Administrator" : "Normal user") + "]";

            updatePublicControls();
        }

       
        private void updatePublicControls()
        {
            List<Address> listA = AddressDAO.getList(); 
            List<Product> listP = ProductDAO.getList();
            List<Order> listO = OrderDAO.getList();
            
            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "address";
            comboBox1.ValueMember = "idaddress";
            comboBox1.DataSource = listA;

            comboBox6.DataSource = null;
            comboBox6.DisplayMember = "name";
            comboBox6.ValueMember = "idproduct";
            comboBox6.DataSource = listP;
            
            comboBox7.DataSource = null;
            comboBox7.DisplayMember = "address";
            comboBox7.ValueMember = "idaddress";
            comboBox7.DataSource = listA;
            
            comboBox8.DataSource = null;
            comboBox8.DisplayMember = "idorder";
            comboBox8.ValueMember = "idorder";
            comboBox8.DataSource = listO;

            dataGridView5.DataSource = null;
            dataGridView5.DataSource = listA;

            dataGridView4.DataSource = null;
            dataGridView4.DataSource = listO;
        }

        private void btnAddAddress_Click(object sender, EventArgs e)
        {
            Address a = new Address();
            a.iduser = user.idUser;
            a.address = richTextBox2.Text;
            
            try
            {
                AddressDAO.addAddress(a);

                MessageBox.Show("Address added successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                richTextBox2.Clear();
                
                updatePublicControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAddress_Click(object sender, EventArgs e)
        {
            try
            {
                AddressDAO.removeAddress((int) (comboBox1.SelectedValue));
                MessageBox.Show("Address deleted successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                updatePublicControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormLogin ventana = new FormLogin();
            ventana.Owner = this;
            this.Hide();
            ventana.ShowDialog();
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Order o = new Order();
            o.idproduct = (int) comboBox6.SelectedValue;
            o.idaddress = (int) comboBox7.SelectedValue;
            o.createdate = dateTimePicker1.Value;
            
            try
            {
                OrderDAO.addOrder(o);

                MessageBox.Show("Order added successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                updatePublicControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            { 
                OrderDAO.removeOrder((int) comboBox8.SelectedValue); 
                MessageBox.Show("Order deleted successfully.", "HUGO", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information); 
                updatePublicControls();
            }
            catch (Exception exception) 
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}