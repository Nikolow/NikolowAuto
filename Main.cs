using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            button2.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client_Area ClientArea = new Client_Area(); 
            ClientArea.Show();
            //this.Close(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login AdminArea = new Login();
            AdminArea.Show();
            //this.Close();
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program_Info Information = new Program_Info();
            Information.Show();
        }

        private void какСеРаботиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Working_Info WI = new Working_Info();
            WI.Show();
        }

    }
}
