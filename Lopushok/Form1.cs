using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lopushok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void login_btn_MouseLeave(object sender, EventArgs e)
        {
            login_btn.BackColor = Color.White;
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm();
            productsForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginAdmin loginAdmin = new LoginAdmin();
            loginAdmin.Show();
            this.Hide();
        }
    }
}
