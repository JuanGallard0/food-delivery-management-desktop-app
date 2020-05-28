using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SourceCode.Modelo;
using LiveCharts;
using LiveCharts.Wpf;
using CartesianChart = LiveCharts.WinForms.CartesianChart;

namespace SourceCode.Vista
{
    public partial class FormMain : Form
    {
        private AppUser user;
        private CartesianChart graficoDemanda;
        public FormMain(AppUser pUser)
        {
            InitializeComponent();
            user = pUser;
            DoubleBuffered = true;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text =
                "Welcome " + user.username + " [" + (user.userType ? "Administrator" : "Normal user") + "]";
            
            graficoDemanda = new CartesianChart();
            this.Controls.Add(graficoDemanda);
            graficoDemanda.Parent = tabControl1.TabPages[4];
            configurarGrafico();
            
            updateAdminControls();
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormLogin ventana = new FormLogin();
            ventana.Owner = this;
            this.Hide();
            ventana.ShowDialog();
            this.Close();
        }
        
        private void configurarGrafico()
        {
            graficoDemanda.Top = 10;
            graficoDemanda.Left = 10;
            graficoDemanda.Width = graficoDemanda.Parent.Width - 20;
            graficoDemanda.Height = graficoDemanda.Parent.Height - 20;

            graficoDemanda.Series = new SeriesCollection
            {
                new ColumnSeries{Title = "Business x Demand.", Values = new ChartValues<int>(), DataLabels = true}
            };

            graficoDemanda.AxisX.Add(new Axis{Labels = new List<string>()});
            graficoDemanda.AxisX[0].Separator = new Separator() {Step = 1, IsEnabled = false};
            graficoDemanda.LegendLocation = LegendLocation.Top;
        }
        
        private void poblarGrafico()
        {
            graficoDemanda.Series[0].Values.Clear();
            graficoDemanda.AxisX[0].Labels.Clear();
          
            foreach (Demand f in OrderDAO.getEstadisticas())
            {
                graficoDemanda.Series[0].Values.Add(f.quantity);
                graficoDemanda.AxisX[0].Labels.Add(f.name);
            }
        }

        private void updateAdminControls()
        {
            List<AppUser> listU = AppUserDAO.getList();
            List<Business> listB = BusinessDAO.getList();
            List<Product> listP = ProductDAO.getList();
            List<FullOrder> listf = OrderDAO.consultOrders();
            
            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = listU;
            
            comboBox2.DataSource = null;
            comboBox2.DisplayMember = "username";
            comboBox2.DataSource = listU;

            comboBox3.DataSource = null;
            comboBox3.DisplayMember = "name";
            comboBox3.DataSource = listB;
            
            comboBox4.DataSource = null; 
            comboBox4.DisplayMember = "name";
            comboBox4.DataSource = listP;
            
            comboBox5.DataSource = null; 
            comboBox5.DisplayMember = "name";
            comboBox5.ValueMember = "idbusiness";
            comboBox5.DataSource = listB;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listU;

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = listB;

            dataGridView3.DataSource = null;
            dataGridView3.DataSource = listP;

            dataGridView4.DataSource = null;
            dataGridView4.DataSource = listf;
            
            poblarGrafico();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AppUser u = new AppUser();
            u.fullname = textBox1.Text;
            u.username = textBox2.Text;
            u.userType = checkBox1.Checked;
            
            try
            {
                AppUserDAO.addAppUser(u);

                MessageBox.Show("User added successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                btnClearUser_Click(sender, e);
                
                updateAdminControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearUser_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            if (checkBox1.Checked)
                checkBox1.Checked = false;
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                AppUserDAO.removeAppUser(comboBox1.Text);
                MessageBox.Show("User deleted successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateAdminControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddBusiness_Click(object sender, EventArgs e)
        {
            Business b = new Business();
            b.name = textBox5.Text;
            b.description = richTextBox1.Text;
            
            try
            {
                BusinessDAO.addBusiness(b);

                MessageBox.Show("Business added successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnClearBusiness_Click_1(sender, e);
                
                updateAdminControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearBusiness_Click_1(object sender, EventArgs e)
        {
            textBox5.Clear();
            richTextBox1.Clear();
        }

        private void btnDeleteBusiness_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessDAO.removeBusiness(comboBox3.Text);
                MessageBox.Show("Business deleted successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateAdminControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.idbusiness = Convert.ToInt32(comboBox5.SelectedValue);
            p.name = textBox6.Text;
         
            try
            {
                ProductDAO.addProduct(p);

                MessageBox.Show("Product added successfully.", "HUGO",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                btnClearProduct_Click(sender, e);
                
                updateAdminControls();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error dialog",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearProduct_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }


        private void btnDeleteProduct_Click_2(object sender, EventArgs e)
        {
             try
             { 
                 ProductDAO.removeProduct(comboBox4.Text); 
                 MessageBox.Show("Product deleted successfully.", "HUGO", 
                     MessageBoxButtons.OK, MessageBoxIcon.Information); 
                 updateAdminControls();
             }
             catch (Exception exception) 
             {
                 MessageBox.Show("Error: " + exception.Message, "Error dialog",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }
        
        
    }
}