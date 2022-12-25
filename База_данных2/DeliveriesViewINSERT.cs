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
    public partial class DeliveriesViewINSERT : Form
    {
        public SqlConnection sqlConnection;

        public DeliveriesViewINSERT(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label5.Visible)
            {
                label5.Visible = false;
            }
            string returnValue = string.Empty;
            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text)
             && !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text)
             && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
             && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
             )

            {
                SqlCommand command = new SqlCommand("[dbo].[Deliveries_Add]", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", 0);
                command.Parameters.AddWithValue("@SupplierId", (int)comboBox1.SelectedValue);
                command.Parameters.AddWithValue("@PartId", (int)comboBox2.SelectedValue);
                command.Parameters.AddWithValue("@Quantity", textBox3.Text);
                command.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Price", textBox1.Text);
                //command.Parameters.AddWithValue("msg", 0);
             
         
        

                // command.Parameters.Add(ret);
                //string ret3 = ret.ToString();
                
                           
                SqlParameter ret = command.Parameters.AddWithValue("@msg",SqlDbType.VarChar);

                ret.Direction = ParameterDirection.Input;

                // SqlParameter retval = new SqlParameter("msg", SqlDbType.VarChar,50);

                // command.Parameters.Add(retval);

                //returnValue = output.Value.ToString();

                //ret.Direction = ParameterDirection.Output;
                comboBox2.Text = "" + ret.ToString();
                await command.ExecuteNonQueryAsync();
                Form1 update = this.Owner as Form1;
                update.update();

                

            }
            /*if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) == null)
            {
                label5.Text = "Укажите цену";*/
        
            else
            {

               
                label5.Visible = true;
                label5.Text = "Поля должны быть заполнены!";//"Поля должны быть заполнены!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeliveriesViewINSERT_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Suppliers". При необходимости она может быть перемещена или удалена.
            this.suppliersTableAdapter.Fill(this.sysorov_IST31DataSet.Suppliers);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Deliveries". При необходимости она может быть перемещена или удалена.
            this.deliveriesTableAdapter.Fill(this.sysorov_IST31DataSet.Deliveries);

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8)
            {
                e.Handled = true;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8)
            {
                e.Handled = true;

            }
        }
    }
}
