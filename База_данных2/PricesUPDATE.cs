using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace База_данных2
{
    public partial class PricesUPDATE : Form
    {
        public SqlConnection sqlConnection = null;
        public int id;

        public PricesUPDATE(SqlConnection connection, int id)
        {
            InitializeComponent();

            sqlConnection = connection;
            this.id = id;
        }
        public async void PricesUPDATE_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);
            SqlCommand getPricesInfoCommand = new SqlCommand("SELECT [Date], [Value], [PartId] FROM [Prices] WHERE [Id]=@id", sqlConnection);
            getPricesInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getPricesInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    dateTimePicker1.Text = Convert.ToString(sqlReader["Date"]);
                    textBox2.Text = Convert.ToString(sqlReader["Value"]);
                    comboBox1.SelectedValue = Convert.ToString(sqlReader["PartId"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            if (label4.Visible)
            {
                label4.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand updatePartscommand = new SqlCommand("UPDATE [Prices] SET [Date] = @Date, [Value] = @Value, [PartId] = @PartId WHERE [Id] = @id", sqlConnection);
                updatePartscommand.Parameters.AddWithValue("Id", id);
                updatePartscommand.Parameters.AddWithValue("Date", dateTimePicker1.Value);
                updatePartscommand.Parameters.AddWithValue("Value", textBox2.Text);
                updatePartscommand.Parameters.AddWithValue("PartId", (int)comboBox1.SelectedValue);

                try
                {
                    await updatePartscommand.ExecuteNonQueryAsync();
                    Form1 update = this.Owner as Form1;
                    update.update();
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                label4.Visible = true;
                label4.Text = "Поля должны быть заполнены!";
            }
        }


        public void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 46)
            {
                e.Handled = true;

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8)
            {
                e.Handled = true;

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8)
            {
                e.Handled = true;

            }
        }
    }
}
