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
    public partial class Admin_Clients : Form
    {
        public Admin_Clients()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manuel_Clients mm = new Manuel_Clients();
            mm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Име!");
                textBox2.Focus();
                return;
            }

            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Телефон за връзка!");
                textBox1.Focus();
                return;
            }


            SqlConnection con = new SqlConnection(Program.source);
            con.Open();
            string s = "select max(id) as Id from [dbo].[CarOwner]";
            SqlCommand cmd2 = new SqlCommand(s, con);
            SqlDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            int i = Convert.ToInt16(dr["Id"].ToString());
            con.Close();

            Owner added = new Owner((i + 1), textBox2.Text, textBox1.Text);
            added.Add_Data_DataBase();

            // CLEAR THEM ALL
            textBox1.Text = "";
            textBox2.Text = "";

            MessageBox.Show("Клиентът беше успешно добавен!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin_Choose mm = new Admin_Choose();
            mm.Show();
        }
    }
}
