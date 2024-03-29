using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lopushok
{
    public partial class MaterialProducts : Form
    {

        DataBase dataBase = new DataBase();
        private readonly CheckUser _user;

        public MaterialProducts(CheckUser user)
        {
            InitializeComponent();
            _user = user;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductsAdmin productsAdmin = new ProductsAdmin(_user);
            productsAdmin.Show();
            this.Hide();
        }

        private void MaterialProducts_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);

            label2.Text = $"{_user.Login}:{_user.Status}";
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("products", "Продукт");
            dataGridView1.Columns.Add("nameproduct", "Материал");
            dataGridView1.Columns.Add("countproduct", "Количество");
            dataGridView1.Columns.Add("id_material", "Номер материала");
        }

        public void ReadSingleRow(DataGridView gridView, IDataRecord record)
        {
            gridView.Rows.Add(record.GetString(0),record.GetString(1),record.GetInt64(2),record.GetInt64(3));
        }

        public void RefreshDataGrid(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string queryString = $"select * from productmaterial";

            NpgsqlCommand command = new NpgsqlCommand(queryString,dataBase.GetConnection());

            dataBase.OpenConnection();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView,reader);
            }
            reader.Close();
        }
    }
}
