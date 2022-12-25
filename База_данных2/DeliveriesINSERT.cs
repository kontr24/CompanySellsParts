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
    public partial class DeliveriesINSERT : Form
    {
        private SqlConnection sqlConnection;
        public DeliveriesINSERT(SqlConnection connection)
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

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
               /* && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)*/
             )

            {
                
                SqlCommand command = new SqlCommand("INSERT INTO [Deliveries] (SupplierId,PartId,Quantity,Date) VALUES (@SupplierId,@PartId,@Quantity,@Date)", sqlConnection);
                command.Parameters.AddWithValue("SupplierId", textBox1.Text);
                command.Parameters.AddWithValue("PartId", textBox2.Text);
                command.Parameters.AddWithValue("Quantity", textBox3.Text);
                command.Parameters.AddWithValue("Date", dateTimePicker1.Value);

                await command.ExecuteNonQueryAsync();
                /*Form1 insert = new Form1();
                insert.update();*/

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
            if ((e.KeyChar <= 47 || e.KeyChar >= 58)  && number != 8)
            {
                e.Handled = true;

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58)  && number != 8)
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
