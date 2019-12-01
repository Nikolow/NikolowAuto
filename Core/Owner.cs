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
using System.IO;

namespace WindowsFormsApp1
{
    class Owner
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Owner(int id, string name, string phone)
        {
            this.ID = id;
            this.Name = name;
            this.Phone = phone;
        }

        public void Add_Data_DataBase()
        {
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT into [dbo].[CarOwner](Name,Phone) Values (N'" + this.Name + "',N'" + this.Phone + "')", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
