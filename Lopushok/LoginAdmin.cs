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

namespace Lopushok
{
    public partial class LoginAdmin : Form
    {

       DataBase dataBase = new DataBase();

        public LoginAdmin()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            var login = textBox1.Text;
            var password = textBox2.Text;

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            DataTable table = new DataTable();

            var query = $"select id_user, login_column, password_column, id_role from roleuser_tbl where login_column = '{login}' and password_column = '{password}'";

            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, dataBase.GetConnection());

            adapter.SelectCommand = npgsqlCommand;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {

                var user = new CheckUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));

                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductsAdmin productsAdmin = new ProductsAdmin(user);
                productsAdmin.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            dataBase.CloseConnection();
        }
    }
}
