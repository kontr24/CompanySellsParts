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

    public partial class PartsINSERT : Form
    {
        public SqlConnection sqlConnection = null;
        /* Form1 frm { get { return this.Owner as Form1; } }*/
        public PartsINSERT(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;

        }
        public async void button1_Click(object sender, EventArgs e)
        {

            if (label5.Visible)
            {
                label5.Visible = false;
            }

            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Parts] (Name,Article, Note) VALUES (@Name,@Article, @Note)", sqlConnection);
                command.Parameters.AddWithValue("Name", comboBox1.Text);
                command.Parameters.AddWithValue("Article", textBox2.Text);
                command.Parameters.AddWithValue("Note", textBox3.Text);

                await command.ExecuteNonQueryAsync();

                Form1 update = this.Owner as Form1;
                update.update();
                Close();
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

        private void PartsINSERT_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);

        }
    }
}
