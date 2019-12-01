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
    public partial class Manuel_AD : Form
    {
        public Manuel_AD()
        {
            InitializeComponent();
        }

        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            //this.tableTableAdapter.Fill(this.database1DataSet.Table);

            Update();
        }

        public void Update()
        {
            SqlConnection con = new SqlConnection(Program.source);

            string query = "select * from [dbo].[Table]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            tableDataGridView.DataSource = dt;
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

            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Table] WHERE Id = " + textBox1.Text, con);
            //DELETE FROM [dbo].[Table] WHERE Id = 1 LIMIT 1
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Записът беше успешно изтрит !", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Update();
        }

        /*private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tableDataGridView.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
            }
        }*/

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin_ADs mm = new Admin_ADs();
            mm.Show();
        }

        String CarName = "", Model = "", Title = "", Money = "", Type = "", Engine = "", Transmition = "", KM = "", State = "", RealiseDate = "", HP = "", Color = "",
               Owner = "", Info = "", Residence = "", AUTOBG = "", MOBILEBG = "", CARSBG = "";
        private void tableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tableDataGridView.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();

                CarName = row.Cells[1].Value.ToString();
                Model = row.Cells[2].Value.ToString();
                Title = row.Cells[3].Value.ToString();
                Money = row.Cells[4].Value.ToString();
                Type = row.Cells[5].Value.ToString();
                Engine = row.Cells[6].Value.ToString();
                Transmition = row.Cells[7].Value.ToString();
                KM = row.Cells[8].Value.ToString();
                State = row.Cells[9].Value.ToString();
                RealiseDate = row.Cells[10].Value.ToString();
                HP = row.Cells[11].Value.ToString();
                Color = row.Cells[12].Value.ToString();
                Owner = row.Cells[13].Value.ToString();
                Info = row.Cells[14].Value.ToString();
                Residence = row.Cells[15].Value.ToString();
                AUTOBG = row.Cells[16].Value.ToString();
                MOBILEBG = row.Cells[17].Value.ToString();
                CARSBG = row.Cells[18].Value.ToString();
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

            try
            {

                SqlConnection con = new SqlConnection(Program.source);

                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Table] SET CarName=N'" + CarName + "', Model=N'" + Model + "', Title=N'" + Title +
                                                                                      "', Money=N'" + Money + "', Type=N'" + Type + "', Engine=N'" + Engine +
                                                                                      "', Transmition=N'" + Transmition + "', KM=N'" + KM + "', State=N'" + State +
                                                                                      "', RealiseDate=N'" + RealiseDate + "', HP=N'" + HP + "', Color=N'" + Color +
                                                                                      /*"', Owner=N'" + Owner + */"', Info=N'" + Info + "', Residence=N'" + Residence +
                                                                                      "', AUTOBG=N'" + AUTOBG + "', MOBILEBG=N'" + MOBILEBG + "', CARSBG=N'" + CARSBG +
                                                                                      "' WHERE Id = " + textBox1.Text, con);
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Записът беше успешно Обновен !", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Update();
        }
    }
}
