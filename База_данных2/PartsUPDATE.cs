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
    public partial class PartsUPDATE : Form
    {

        private SqlConnection sqlConnection = null;
        private int id;
        public PartsUPDATE(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void PartsUPDATE_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "sysorov_IST31DataSet.Parts". При необходимости она может быть перемещена или удалена.
            this.partsTableAdapter.Fill(this.sysorov_IST31DataSet.Parts);
            SqlCommand getPartsInfoCommand = new SqlCommand("SELECT [Name], [Article], [Note] FROM [Parts] WHERE [Id]=@id", sqlConnection);
            getPartsInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getPartsInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    comboBox1.Text = Convert.ToString(sqlReader["Name"]);
                    textBox2.Text = Convert.ToString(sqlReader["Article"]);
                    textBox3.Text = Convert.ToString(sqlReader["Note"]);

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
                SqlCommand updatePartscommand = new SqlCommand("UPDATE [Parts] SET [Name] = @Name, [Article] = @Article, [Note] = @Note WHERE [Id] = @id", sqlConnection);
                updatePartscommand.Parameters.AddWithValue("Id", id);
                updatePartscommand.Parameters.AddWithValue("Name", comboBox1.Text);
                updatePartscommand.Parameters.AddWithValue("Article", textBox2.Text);
                updatePartscommand.Parameters.AddWithValue("Note", textBox3.Text);
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
    }
}
