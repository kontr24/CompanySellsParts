using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace База_данных2
{
    public partial class Form1 : Form
    {
        public SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\Sysorov_IST31.mdf;Integrated Security = True"; /*User ID = sa; Password = 123456;*/
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;


            listView1.Columns.Add("Id");
            listView1.Columns.Add("Дата");
            listView1.Columns.Add("Цена");
            listView1.Columns.Add("Детали");

            listView1.Columns[listView1.Columns.Count - 1].Width = 170;
            listView1.Columns[listView1.Columns.Count - 2].Width = 120;
            listView1.Columns[listView1.Columns.Count - 3].Width = 100;
            listView1.Columns[listView1.Columns.Count - 4].Width = 0;

            /*listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumn(2,ColumnHeaderAutoResizeStyle.HeaderSize);*/

            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.View = View.Details;

            listView2.Columns.Add("Id");
            listView2.Columns.Add("Название");
            listView2.Columns.Add("Артикул");
            listView2.Columns.Add("Примечание");

            listView2.Columns[listView2.Columns.Count - 1].Width = 170;
            listView2.Columns[listView2.Columns.Count - 2].Width = 150;
            listView2.Columns[listView2.Columns.Count - 3].Width = 170;
            listView2.Columns[listView2.Columns.Count - 4].Width = 0;


            listView3.GridLines = true;
            listView3.FullRowSelect = true;
            listView3.View = View.Details;

            listView3.Columns.Add("Id");
            listView3.Columns.Add("Название");
            listView3.Columns.Add("Адрес");
            listView3.Columns.Add("Телефон");

            listView3.Columns[listView3.Columns.Count - 1].Width = 130;
            listView3.Columns[listView3.Columns.Count - 2].Width = 390;
            listView3.Columns[listView3.Columns.Count - 3].Width = 130;
            listView3.Columns[listView3.Columns.Count - 4].Width = 0;


            listView5.GridLines = true;
            listView5.FullRowSelect = true;
            listView5.View = View.Details;

            listView5.Columns.Add("Id");
            listView5.Columns.Add("Название поставщика");
            listView5.Columns.Add("Адрес поставщика");
            listView5.Columns.Add("Телефон поставщика");
            listView5.Columns.Add("Количество");
            listView5.Columns.Add("Дата");
            listView5.Columns.Add("Название детали");
            listView5.Columns.Add("Артикул детали");
            listView5.Columns.Add("Примечание к детали");
            listView5.Columns.Add("Цена детали");

            listView5.Columns[listView5.Columns.Count - 1].Width = 120;
            listView5.Columns[listView5.Columns.Count - 2].Width = 170;
            listView5.Columns[listView5.Columns.Count - 3].Width = 150;
            listView5.Columns[listView5.Columns.Count - 4].Width = 170;
            listView5.Columns[listView5.Columns.Count - 5].Width = 100;
            listView5.Columns[listView5.Columns.Count - 6].Width = 105;
            listView5.Columns[listView5.Columns.Count - 7].Width = 170;
            listView5.Columns[listView5.Columns.Count - 8].Width = 390;
            listView5.Columns[listView5.Columns.Count - 9].Width = 170;
            listView5.Columns[listView5.Columns.Count - 10].Width = 0;

            await LoadTableAsync();
        }


        public async Task LoadTableAsync()
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT Prices.Id, Prices.Date, Prices.Value, Parts.Name from Parts join Prices on Prices.PartId=Parts.id", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["id"]),
                        Convert.ToString(String.Format("{0:dd/MM/yyyy}", sqlReader["Date"])),
                        Convert.ToString(sqlReader["Value"]),
                        Convert.ToString(sqlReader["Name"])
                        });

                    listView1.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }

            SqlDataReader sqlReader1 = null;
            SqlCommand command1 = new SqlCommand("SELECT * FROM [Parts]", sqlConnection);
            try
            {
                sqlReader1 = await command1.ExecuteReaderAsync();
                while (await sqlReader1.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                         Convert.ToString(sqlReader1["id"]),
                         Convert.ToString(sqlReader1["Name"]),
                         Convert.ToString(sqlReader1["Article"]),
                         Convert.ToString(sqlReader1["Note"])

                         });

                    listView2.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader1 != null)
                {
                    sqlReader1.Close();
                }
            }
            SqlDataReader sqlReader2 = null;
            SqlCommand command2 = new SqlCommand("SELECT * FROM [Suppliers]", sqlConnection);
            try
            {
                sqlReader2 = await command2.ExecuteReaderAsync();
                while (await sqlReader2.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                         Convert.ToString(sqlReader2["id"]),
                         Convert.ToString(sqlReader2["Name"]),
                         Convert.ToString(sqlReader2["Address"]),
                         Convert.ToString(sqlReader2["Phone"])

                         });

                    listView3.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader2 != null)
                {
                    sqlReader2.Close();
                }
            }

            SqlDataReader sqlReader4 = null;
            SqlCommand command4 = new SqlCommand("SELECT * FROM [DeliveriesView]", sqlConnection);
            try
            {
                sqlReader4 = await command4.ExecuteReaderAsync();
                while (await sqlReader4.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader4["id"]),
                        Convert.ToString(sqlReader4["SupplierName"]),
                        Convert.ToString(sqlReader4["SupplierAddress"]),
                        Convert.ToString(sqlReader4["SupplierPhone"]),
                        Convert.ToString(sqlReader4["Quantity"]),
                        Convert.ToString(String.Format("{0:dd/MM/yyyy}", sqlReader4["Date"])),
                        Convert.ToString(sqlReader4["PartName"]),
                        Convert.ToString(sqlReader4["PartArticle"]),
                        Convert.ToString(sqlReader4["PartNote"]),
                        Convert.ToString(sqlReader4["Price"])
                        });

                    listView5.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader4 != null && !sqlReader4.IsClosed)
                {
                    sqlReader4.Close();
                }
            }

        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        public void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
            Application.Exit();
        }
        // Первое

        //детали
        private void button4_Click(object sender, EventArgs e)
        {
            PartsINSERT insert = new PartsINSERT(sqlConnection);

            insert.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                PartsUPDATE update = new PartsUPDATE(sqlConnection, Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text));
                update.Show(this);
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {

                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand deletePartsCommand = new SqlCommand("DELETE FROM [Parts] WHERE [Id] =@id", sqlConnection);
                        deletePartsCommand.Parameters.AddWithValue("id", Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text));
                        try
                        {

                            await deletePartsCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Для начала необходимо удалить все зависимые записи в других таблицах!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            /*MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                        }

                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        listView3.Items.Clear();
                        listView5.Items.Clear();
                        await LoadTableAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public async void update()
        {
            listView5.Items.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            await LoadTableAsync();
        }
        // конец

        public void button8_Click(object sender, EventArgs e)  //Для цены
        {
            PricesINSERT insert = new PricesINSERT(sqlConnection);

            insert.Show(this);
        }

        public void button9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                PricesUPDATE update = new PricesUPDATE(sqlConnection, Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                update.Show(this);
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand deletePricesCommand = new SqlCommand("DELETE FROM [Prices] WHERE [Id] =@id", sqlConnection);
                        deletePricesCommand.Parameters.AddWithValue("id", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                        try
                        {

                            await deletePricesCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        listView3.Items.Clear();
                        listView5.Items.Clear();
                        await LoadTableAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Для цены

        private void button12_Click(object sender, EventArgs e) //поставщики
        {
            SuppliersINSERT insert = new SuppliersINSERT(sqlConnection);

            insert.Show(this);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            if (listView3.SelectedItems.Count > 0)
            {
                SuppliersUPDATE update = new SuppliersUPDATE(sqlConnection, Convert.ToInt32(listView3.SelectedItems[0].SubItems[0].Text));
                update.Show(this);
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {

                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand deleteSuppliersCommand = new SqlCommand("DELETE FROM [Suppliers] WHERE [Id] =@id", sqlConnection);
                        deleteSuppliersCommand.Parameters.AddWithValue("id", Convert.ToInt32(listView3.SelectedItems[0].SubItems[0].Text));
                        try
                        {

                            await deleteSuppliersCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Для начала необходимо удалить все зависимые записи в других таблицах!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            /*MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                        }

                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        listView3.Items.Clear();
                        listView5.Items.Clear();
                        await LoadTableAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // поставщики

        private void button1_Click(object sender, EventArgs e)
        {
            DeliveriesViewINSERT insert = new DeliveriesViewINSERT(sqlConnection);

            insert.Show(this);
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            if (listView5.SelectedItems.Count > 0)
            {

                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку?", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand deleteDeliveriesViewCommand = new SqlCommand("delete from Deliveries where Id = @id", sqlConnection);
                        deleteDeliveriesViewCommand.Parameters.AddWithValue("id", Convert.ToInt32(listView5.SelectedItems[0].SubItems[0].Text));
                        try
                        {

                            await deleteDeliveriesViewCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Невозможно удалить запись в таблице 'Поставки', так как изменение влияет на несколько базовых таблиц!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        listView3.Items.Clear();
                        listView5.Items.Clear();
                        await LoadTableAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView5.Items.Clear();
            await LoadTableAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView5.SelectedItems.Count > 0)
            {
                DeliveriesViewIUPDATE update = new DeliveriesViewIUPDATE(sqlConnection, Convert.ToInt32(listView5.SelectedItems[0].SubItems[0].Text));
                update.Show(this);
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*
            MessageBox.Show("Нельзя редактировать строку,так как представление ссылается на несколько базовых таблиц!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
        }
        //поставки

    }

}
