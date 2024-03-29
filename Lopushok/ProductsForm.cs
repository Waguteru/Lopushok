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
//using static System.Net.Mime.MediaTypeNames;
//using Application = System.Windows.Forms.Application;

namespace Lopushok
{
    public partial class ProductsForm : Form
    {

        DataBase dataBase = new DataBase();

        enum RowState
        {
            Existed,
            New,
            Modfied,
            ModfiedNew,
            Deleted
        }

        public ProductsForm()
        {
            InitializeComponent();
        }

        private void вернутьсяКАвторизацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.Show();
            this.Hide();
        }

        public void CreateColumns()
        {
            dataGridView1.Columns.Add("nameproduct", "название"); //0
            dataGridView1.Columns.Add("article", "артикул"); //1
            dataGridView1.Columns.Add("mincost", "цена"); //2
            dataGridView1.Columns.Add("image", "изображение"); //3
            dataGridView1.Columns.Add("producttype", "тип продукции"); //4
         //   dataGridView1.Columns["image"].Visible = false;
        }

        public void ReadSingleRows(DataGridView gridView,IDataRecord record)
        {
            gridView.Rows.Add(record.GetString(0),record.GetInt64(1),record.GetString(2),record.GetString(3),record.GetString(4),RowState.ModfiedNew);
        }

        private void RefreshDataGrid(DataGridView dataGrid)
        {
            dataGrid.Rows.Clear();

            string queryString = $"select nameproduct,article,mincost,image,producttype from products";

            NpgsqlCommand command = new NpgsqlCommand(queryString, dataBase.GetConnection());

            dataBase.OpenConnection();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(dataGrid,reader);
            }
            reader.Close();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void Search(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string queryString = $"select nameproduct,article,mincost,producttype,image from products where concat (nameproduct) like '%" + textBox1.Text + "%'";

            NpgsqlCommand command = new NpgsqlCommand( queryString, dataBase.GetConnection());

           dataBase.OpenConnection( );

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(gridView,reader);
            }
            reader.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedItem == "наименование(По возрастанию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[1],ListSortDirection.Ascending);
            }
            if(comboBox2.SelectedItem == "наименование(По убыванию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[1],ListSortDirection.Descending);
            }

          /*  if(comboBox2.SelectedItem == "минимальная стоимость(По возрастанию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[3],ListSortDirection.Ascending);
            }
            if (comboBox2.SelectedItem == "минимальная стоимость(По убыванию)")
            {
                dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
            }*/
        }

        private void AllType(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection( );

            string query = $"select  nameproduct,article,mincost,image,producttype from products";

            NpgsqlCommand command = new NpgsqlCommand( query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while( Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void OneLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select nameproduct,article,mincost,image,producttype from products where producttype LIKE 'Один слой' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void TwoLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select nameproduct,article,mincost,image,producttype from products where producttype LIKE 'Два слоя' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void ThreeLayer(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select nameproduct,article,mincost,image,producttype from products where producttype LIKE 'Три слоя' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void SuperSoft(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select nameproduct,article,mincost,image,producttype from products where producttype LIKE 'Супер мягкая' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        public void Childish(DataGridView gridView)
        {
            gridView.Rows.Clear();

            dataBase.OpenConnection();

            string query = $"select nameproduct,article,mincost,image,producttype from products where producttype LIKE 'Детская' ";

            NpgsqlCommand command = new NpgsqlCommand(query, dataBase.GetConnection());

            NpgsqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                ReadSingleRows(gridView, Reader);
            }
            Reader.Close();
            dataBase.CloseConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == "Все типы")
            {
                AllType(dataGridView1);
            }
            if(comboBox1.SelectedItem == "Один слой")
            {
                OneLayer(dataGridView1);
            }
            if(comboBox1.SelectedItem == "Два слоя")
            {
                TwoLayer(dataGridView1);
            }
            if (comboBox1.SelectedItem == "Три слоя")
            {
                ThreeLayer(dataGridView1);
            }
            if(comboBox1.SelectedItem == "Супер мягкая")
            {
                SuperSoft(dataGridView1);
            }
            if(comboBox1.SelectedItem == "Детская")
            {
                Childish(dataGridView1);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string imagePath = row.Cells[3].Value.ToString();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    pictureBox1.ImageLocation = imagePath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show("Изображение не найдено");
                }

            }
        }
    }
}
