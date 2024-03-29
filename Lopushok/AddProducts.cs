using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lopushok
{
    public partial class AddProducts : Form
    {

        DataBase dataBase = new DataBase();
        private readonly CheckUser _user;

        public AddProducts(CheckUser user)
        {
            InitializeComponent();
            _user = user;

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox4.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductsAdmin productsAdmin = new ProductsAdmin(_user);
            productsAdmin.Show();
            this.Hide();
        }

        public string RandonArticul()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        private void AddProducts_Load(object sender, EventArgs e)
        {
            label3.Text = $"{_user.Login}:{_user.Status}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            var nameProduct = textBox1.Text;
            // var articul = textBox2.Text;
            string articul = RandonArticul();
            var mincount = textBox3.Text;
            var image = textBox4.Text;
            var type = comboBox1.Text;
            var people = textBox7.Text;
            var workshop = textBox6.Text;

            string query = $"insert into products (nameproduct,article,mincost,image,producttype,numberofpeople,numberworkshop) values ('{nameProduct}', '{articul}', '{mincount}', '{image}', '{type}', '{people}', '{workshop}')";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            command.ExecuteNonQuery();

            MessageBox.Show("Товар успешно добавлен!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dataBase.CloseConnection();

            ProductsAdmin productsAdmin = new ProductsAdmin(_user);
            productsAdmin.Show();
            this.Hide();
        }
    }
}
