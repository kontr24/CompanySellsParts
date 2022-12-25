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
    public partial class SuppliersUPDATE : Form
    {

        private SqlConnection sqlConnection = null;
        private int id;
        public SuppliersUPDATE(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void SuppliersUPDATE_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Suppliers". При необходимости она может быть перемещена или удалена.
            this.suppliersTableAdapter.Fill(this.sysorov_IST31DataSet.Suppliers);
            SqlCommand getSuppliersInfoCommand = new SqlCommand("SELECT [Name], [Address], [Phone] FROM [Suppliers] WHERE [Id]=@id", sqlConnection);
            getSuppliersInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getSuppliersInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox1.Text = Convert.ToString(sqlReader["Name"]);
                    textBox2.Text = Convert.ToString(sqlReader["Address"]);
                    textBox3.Text = Convert.ToString(sqlReader["Phone"]);

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
                SqlCommand updatePartscommand = new SqlCommand("UPDATE [Suppliers] SET [Name] = @Name, [Address] = @Address, [Phone] = @Phone WHERE [Id] = @id", sqlConnection);
                updatePartscommand.Parameters.AddWithValue("Id", id);
                updatePartscommand.Parameters.AddWithValue("Name", comboBox1.Text);
                updatePartscommand.Parameters.AddWithValue("Address", textBox2.Text);
                updatePartscommand.Parameters.AddWithValue("Phone", textBox3.Text);
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
    }
}
