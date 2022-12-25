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
    public partial class DeliveriesViewIUPDATE : Form
    {
        public SqlConnection sqlConnection = null;
        public int id;
        public DeliveriesViewIUPDATE(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void DeliveriesViewIUPDATE_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Suppliers". При необходимости она может быть перемещена или удалена.
            this.suppliersTableAdapter.Fill(this.sysorov_IST31DataSet.Suppliers);
            SqlCommand getDeliveriesViewInfoCommand = new SqlCommand("SELECT [SupplierName], [PartName],[Quantity], [Date] FROM [DeliveriesView] WHERE [Id]=@id", sqlConnection);
            

            getDeliveriesViewInfoCommand.Parameters.AddWithValue("Id", id);


            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getDeliveriesViewInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox2.Text = Convert.ToString(sqlReader["SupplierName"]);
                    comboBox2.Text = Convert.ToString(sqlReader["PartName"]);
                    textBox3.Text = Convert.ToString(sqlReader["Quantity"]);
                    dateTimePicker1.Text = Convert.ToString(sqlReader["Date"]);
                   

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

            if (label5.Visible)
            {
                label5.Visible = false;
            }

            if (/*comboBox1.SelectedText.Length > 0 && comboBox2.SelectedText.Length > 0*/
                !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text)
             && !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text)
             && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
             )
            {
                SqlCommand updateDeliveriescommand = new SqlCommand("UPDATE [Deliveries] SET [SupplierId] = @SupplierId, [PartId] = @PartId, [Quantity] = @Quantity, [Date] = @Date WHERE [Id] = @id", sqlConnection);
                updateDeliveriescommand.Parameters.AddWithValue("Id", id);
                updateDeliveriescommand.Parameters.AddWithValue("SupplierId", (int)comboBox1.SelectedValue);
                updateDeliveriescommand.Parameters.AddWithValue("PartId", (int)comboBox2.SelectedValue);
                updateDeliveriescommand.Parameters.AddWithValue("Quantity", textBox3.Text);
                updateDeliveriescommand.Parameters.AddWithValue("Date", dateTimePicker1.Value);




                try
                {
                    await updateDeliveriescommand.ExecuteNonQueryAsync();
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
                label5.Visible = true;
                label5.Text = "Поля должны быть заполнены!";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();

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
