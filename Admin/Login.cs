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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Program.source);
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Login where Username ='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                 this.Close();
                 Admin_Choose mm = new Admin_Choose();
                 mm.Show();
            }
            else
            {
                MessageBox.Show("Невалидни данни!\nМоля проверете вашите данни или се свържете с Администратор!", "Грешка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
