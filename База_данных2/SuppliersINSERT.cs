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
    public partial class SuppliersINSERT : Form
    {
        private SqlConnection sqlConnection;
        public SuppliersINSERT(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label4.Visible)
            {
                label4.Visible = false;
            }

            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Suppliers] (Name,Address, Phone) VALUES (@Name,@Address, @Phone)", sqlConnection);
                command.Parameters.AddWithValue("Name", comboBox1.Text);
                command.Parameters.AddWithValue("Address", textBox2.Text);
                command.Parameters.AddWithValue("Phone", textBox3.Text);

                await command.ExecuteNonQueryAsync();

                Form1 update = this.Owner as Form1;
                update.update();
                Close();
            }
            else
            {
                label4.Visible = true;
                label4.Text = "Поля должны быть заполнены!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 43 && number != 45)
            {
                e.Handled = true;

            }
        }

        private void SuppliersINSERT_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Suppliers". При необходимости она может быть перемещена или удалена.
            this.suppliersTableAdapter.Fill(this.sysorov_IST31DataSet.Suppliers);

        }
    }
}
