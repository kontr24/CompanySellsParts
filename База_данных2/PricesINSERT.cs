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
    public partial class PricesINSERT : Form

    {
         SqlConnection sqlConnection;
        public PricesINSERT(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;

            PricesUPDATE update1 = new PricesUPDATE(sqlConnection, 0);
            update1.Owner = this;
        }
        public async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
            {
                label6.Visible = false;
            }
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Prices] (Date,Value, PartId) VALUES (@Date,@Value, @PartId)", sqlConnection);
                command.Parameters.AddWithValue("Date", dateTimePicker1.Value);
                command.Parameters.AddWithValue("Value", textBox2.Text);
                command.Parameters.AddWithValue("PartId", (int)comboBox1.SelectedValue);

                await command.ExecuteNonQueryAsync();

                Form1 update = this.Owner as Form1;
                update.update();
                Close();
            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля должны быть заполнены!";
            }
        }
        public void button2_Click_1(object sender, EventArgs e)
        { 
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58)  && number != 8 && number != 46)
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

        private void PricesINSERT_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet1.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter1.Fill(this.sysorov_IST31DataSet1.Parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Deliveries". При необходимости она может быть перемещена или удалена.
            this.deliveriesTableAdapter.Fill(this.sysorov_IST31DataSet.Deliveries);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Prices". При необходимости она может быть перемещена или удалена.
            this.pricesTableAdapter.Fill(this.sysorov_IST31DataSet.Prices);

        }
    }
}
