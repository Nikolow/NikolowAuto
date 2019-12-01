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
    public partial class Owners : Form
    {
        public Owners()
        {
            InitializeComponent();
        }

        private void carOwnerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carOwnerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.CarOwner' table. You can move, or remove it, as needed.
            this.carOwnerTableAdapter.Fill(this.database1DataSet.CarOwner);

        }
    }
}
