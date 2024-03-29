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
    public partial class ProductsAdmin : Form
    {

        DataBase dataBase = new DataBase();
        private readonly CheckUser _user;

        enum RowState
        {
            Exited,
            New,
            Modifided,
            ModifidedNew,
            Deleted
        }

        int selectedRow;


        public ProductsAdmin(CheckUser user)
        {
            InitializeComponent();
            _user = user;
          
        }

        public ProductsAdmin()
        {
           
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            deleteBtn.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void ChangeBtn_MouseEnter(object sender, EventArgs e)
        {
            ChangeBtn.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void SafeBtn_MouseEnter(object sender, EventArgs e)
        {
            SafeBtn.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void deleteBtn_MouseLeave(object sender, EventArgs e)
        {
            deleteBtn.BackColor = Color.White;
        }

        private void ChangeBtn_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtn.BackColor = Color.White;
        }

        private void SafeBtn_MouseLeave(object sender, EventArgs e)
        {
            SafeBtn.BackColor = Color.White;
        }

        private void ProductsAdmin_Load(object sender, EventArgs e)
        {
            CreateColumn();
            RefreshDataGrid(dataGridView1);

            label13.Text = $"{ _user.Login}:{_user.Status}";
        }

        public void CreateColumn()
        {
            dataGridView1.Columns.Add("nameproduct", "Название");//0
            dataGridView1.Columns.Add("article", "Артикул"); //1
            dataGridView1.Columns.Add("mincost", "Цена"); //2
            dataGridView1.Columns.Add("image", "Изображение"); //3
            dataGridView1.Columns.Add("producttype", "Тип продукта"); //4
            dataGridView1.Columns.Add("numberofpeople", "Кол-во человек"); //5
            dataGridView1.Columns.Add("numberworkshop", "Цех"); //6
            dataGridView1.Columns.Add("id_products", "Номер продукта"); //7
            dataGridView1.Columns.Add("isNew", String.Empty); //8
            dataGridView1.Columns["isNew"].Visible = false;
        }

        public void ReadSingleRow(DataGridView gridView, IDataRecord record)
        {
            gridView.Rows.Add(record.GetString(0), record.GetInt64(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetInt64(5), record.GetString(6), record.GetInt64(7));
        }

        public void RefreshDataGrid(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string queryString = $"select * from products";

            NpgsqlCommand command = new NpgsqlCommand(queryString, dataBase.GetConnection());

            dataBase.OpenConnection();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string path = row.Cells[3].Value.ToString();

                if (!string.IsNullOrEmpty(path))
                {
                    pictureBox1.ImageLocation = path;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("изображение не найдено");
                }
            }

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                namrproducts_tb.Text = row.Cells[0].Value.ToString();
                articul_tb.Text = row.Cells[1].Value.ToString();
                price_tb.Text = row.Cells[2].Value.ToString();
                image_tb.Text = row.Cells[3].Value.ToString();
                comboBox3.Text = row.Cells[4].Value.ToString();
                coutpupl_tb.Text = row.Cells[5].Value.ToString();
                workshop_tb.Text = row.Cells[6].Value.ToString();
                idproducts_tb.Text = row.Cells[7].Value.ToString();
            }
        }

        public void Search(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string queryString = $"select * from products where concat (nameproduct) like '%" + textBox1.Text + "%'";

            NpgsqlCommand cmd = new NpgsqlCommand(queryString, dataBase.GetConnection());

            dataBase.OpenConnection();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == "наименование(По возрастанию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            }
            if (comboBox2.SelectedItem == "наименование(По убыванию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Все типы")
            {
                AllType(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Один слой")
            {
                OneLayer(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Два слоя")
            {
                TwoLayer(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Три слоя")
            {
                ThreeLayer(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Супер мягкая")
            {
                SuperSoft(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Детская")
            {
                Childish(dataGridView1);
            }
        }

        private void AllType(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select * from products";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void OneLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select* from products where producttype LIKE 'Один слой' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void TwoLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select * from products where producttype where producttype LIKE 'Два слоя' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void ThreeLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select * from products where producttype LIKE 'Три слоя' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void SuperSoft(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select * from products where producttype LIKE 'Супер мягкая' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void Childish(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select * from products where producttype LIKE 'Детская' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRow(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        public void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            Change();
        }

        public void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var nameProduct = namrproducts_tb.Text;
            var articul = articul_tb.Text;
            var mincount = price_tb.Text;
            var image = image_tb.Text;
            var type = comboBox3.Text;
            var people = coutpupl_tb.Text;
            var workshop = workshop_tb.Text;
            var idPro = idproducts_tb.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(nameProduct, articul, mincount, image, type, people, workshop, idPro);
                dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState.Modifided;
            }
        }

        private void SafeBtn_Click(object sender, EventArgs e)
        {

            dataBase.OpenConnection();

            var nameProducts = namrproducts_tb.Text;
            var articul = Convert.ToInt64(articul_tb.Text);
            var mincount = price_tb.Text;
            var image = image_tb.Text;
            var type = comboBox3.Text;
            var people = Convert.ToInt64(coutpupl_tb.Text);
            var workshop = workshop_tb.Text;
            var idPro = Convert.ToInt64(idproducts_tb.Text);

            string query = $"DELETE FROM products WHERE id_products = " + idPro;

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            command.ExecuteNonQuery();

            dataBase.CloseConnection();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            var nameProducts = namrproducts_tb.Text;
            var articul = Convert.ToInt64(articul_tb.Text);
            var mincount = price_tb.Text;
            var image = image_tb.Text;
            var type = comboBox3.Text;
            var people = Convert.ToInt64(coutpupl_tb.Text);
            var workshop = workshop_tb.Text;
            var idPro = Convert.ToInt64(idproducts_tb.Text);

            string query = $"UPDATE products SET article = '{articul}',mincost = '{mincount}',producttype = '{type}' WHERE id_products = " + idPro;

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            command.ExecuteNonQuery();

            dataBase.CloseConnection();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(0, 204, 118);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddProducts addProducts = new AddProducts(_user);
            addProducts.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaterialProducts materialProducts = new MaterialProducts(_user);
            materialProducts.Show();
            this.Hide();
        }
    }
}
