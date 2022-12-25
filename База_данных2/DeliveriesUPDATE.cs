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
    public partial class DeliveriesUPDATE : Form
    {

        private SqlConnection sqlConnection = null;
        private int id;

        public DeliveriesUPDATE(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }


        private async void DeliveriesUPDATE_Load(object sender, EventArgs e)
        {
            SqlCommand getDeliveriesInfoCommand = new SqlCommand("SELECT [SupplierId], [PartId], [Quantity], [Date] FROM [Deliveries] WHERE [Id]=@id", sqlConnection);
            getDeliveriesInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getDeliveriesInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlReader["SupplierId"]);
                    textBox2.Text = Convert.ToString(sqlReader["PartId"]);
                    textBox3.Text = Convert.ToString(sqlReader["Quantity"]);
                    textBox4.Text = Convert.ToString(sqlReader["Date"]);

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
            SqlCommand updateDeliveriescommand = new SqlCommand("UPDATE [Deliveries] SET [SupplierId] = @SupplierId, [PartId] = @PartId, [Quantity] = @Quantity, [Date] = @Date WHERE [Id] = @id", sqlConnection);
            updateDeliveriescommand.Parameters.AddWithValue("Id", id);
            updateDeliveriescommand.Parameters.AddWithValue("SupplierId", textBox1.Text);
            updateDeliveriescommand.Parameters.AddWithValue("PartId", textBox2.Text);
            updateDeliveriescommand.Parameters.AddWithValue("Quantity", textBox3.Text);
            updateDeliveriescommand.Parameters.AddWithValue("Date", textBox4.Text);

            try
            {
                await updateDeliveriescommand.ExecuteNonQueryAsync();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8)
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 46)
            {
                e.Handled = true;

            }
        }
    }
}
