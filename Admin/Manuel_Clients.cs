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

namespace WindowsFormsApp1
{
    public partial class Manuel_Clients : Form
    {
        public Manuel_Clients()
        {
            InitializeComponent();
        }

        private void carOwnerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carOwnerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.CarOwner' table. You can move, or remove it, as needed.
            this.carOwnerTableAdapter.Fill(this.database1DataSet.CarOwner);

            Update();

        }

        public void Update()
        {
            SqlConnection con = new SqlConnection(Program.source);

            string query = "select * from [dbo].[CarOwner]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            carOwnerDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                textBox1.Focus();
                MessageBox.Show("Внимание! Вие не сте избрали кой ЗАПИС да бъде изтрит!");
                return;
            }

            SqlConnection con = new SqlConnection(Program.source);

            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[CarOwner] WHERE Id = " + textBox1.Text, con);
            //DELETE FROM [dbo].[CarOwner] WHERE Id = 1 LIMIT 1
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Записът беше успешно изтрит !", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Update();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin_Clients mm = new Admin_Clients();
            mm.Show();

            
        }

        String Name = "";
        String Phone = "";
        private void carOwnerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.carOwnerDataGridView.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                Name = row.Cells[1].Value.ToString();
                Phone = row.Cells[2].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                textBox1.Focus();
                MessageBox.Show("Внимание! Вие не сте избрали кой ЗАПИС да бъде Обновен!");
                return;
            }

            SqlConnection con = new SqlConnection(Program.source);

            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[CarOwner] SET Name=N'" + Name + "', Phone=N'" + Phone + "' WHERE Id = " + textBox1.Text, con);
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Записът беше успешно Обновен !", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Update();
        }
    }
}
