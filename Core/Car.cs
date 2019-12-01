using System;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    internal class Car
    {
        public string Carname { get; set; }
        public string Model { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
        public string Transmition { get; set; }
        public string Km { get; set; }
        public string State { get; set; }
        public string Rd { get; set; }
        public string Hp { get; set; }
        public string Color { get; set; }
        public string Info { get; set; }
        public string Residence { get; set; }
        public string Autobg { get; set; }
        public string Mobilebg { get; set; }
        public string Carsbg { get; set; }


        public int ID { get; set; }
        public int Money { get; set; }
        public int Owner { get; set; }
        public byte[] Image { get; set; }


        public Car(int Id, string CarName, string Model, string Title, int Money, byte[] Image, string Type, string Engine, string Transmition, string KM, string State, string RD, string HP, string Color, int Owner, string Info, string Residence, string Autobg, string Mobilebg, string Carsbg)
        {
            this.ID = Id;
            this.Carname = CarName;
            this.Model = Model;
            this.Title = Title;
            this.Money = Money;
            this.Image = Image;
            this.Type = Type;
            this.Engine = Engine;
            this.Transmition = Transmition;
            this.Km = KM;
            this.State = State;
            this.Rd = RD;
            this.Hp = HP;
            this.Color = Color;
            this.Owner = Owner;
            this.Info = Info;
            this.Residence = Residence;
            this.Autobg = Autobg;
            this.Mobilebg = Mobilebg;
            this.Carsbg = Carsbg;
        }

        public void Add_Data_DataBase()
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.source);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT into [dbo].[Table]" +
                  "(Id,CarName,Model,Title,Money,Image,Type,Engine,Transmition,KM,State,RealiseDate,HP,Color,Owner,Info," +
                 "Residence,AUTOBG,MOBILEBG,CARSBG) Values ('"+this.ID+ "','" + this.Carname + "','" + this.Model + "','" + this.Title + "','" + this.Money +
                 "','@Pic','" + this.Type + "','" + this.Engine + "','" + this.Transmition + "','" + this.Km + "','" + this.State + "','" + this.Rd +
                 "','" + this.Hp + "','" + this.Color + "','" + this.Owner + "','" + this.Info + "','" + this.Residence + "','" + this.Autobg + "','" + this.Mobilebg + "','" + this.Carname + "')", con);
                SqlParameter prm = new SqlParameter("@Pic", SqlDbType.VarBinary, this.Image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, this.Image);
                cmd.Parameters.Add(prm);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
