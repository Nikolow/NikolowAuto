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
    public partial class Admin_ADs : Form
    {
        public Admin_ADs()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            textBox2.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Марка!");
                textBox2.Focus();
                return;
            }

            if (textBox3.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Модел!");
                textBox3.Focus();
                return;
            }

            if (textBox4.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Заглавие!");
                textBox4.Focus();
                return;
            } 

            if (textBox5.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Цена!");
                textBox5.Focus();
                return;
            }

            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Не сте избрали Снимка!");
                textBox1.Focus();
                return;
            }

            if (textBox6.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за извъвършени километри!");
                textBox6.Focus();
                return;
            }

            if (textBox7.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за ВИД Употреба!");
                textBox7.Focus();
                return;
            }

            if (textBox8.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Година на производство!");
                textBox8.Focus();
                return;
            }

            if (textBox9.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Конски Сили!");
                textBox9.Focus();
                return;
            }

            if (textBox10.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Цвят!");
                textBox10.Focus();
                return;
            }

            if (textBox11.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за ID на собственик!");
                textBox11.Focus();
                return;
            }

            if (richTextBox1.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Допълнителна Информация!");
                richTextBox1.Focus();
                return;
            }

            if (textBox12.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели данни за Местожителство на колата!");
                textBox12.Focus();
                return;
            }

            if (textBox13.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели линк за AutoBG!");
                textBox13.Focus();
                return;
            }

            if (textBox14.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели линк за MobileBG!");
                textBox14.Focus();
                return;
            }

            if (textBox15.Text.Length < 1)
            {
                MessageBox.Show("Не сте въвели линк за CarsBG!");
                textBox15.Focus();
                return;
            }


            FileStream fs1 = new FileStream(textBox1.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] image = new byte[fs1.Length];
            fs1.Read(image, 0, Convert.ToInt32(fs1.Length));
            fs1.Close();



            String car_type = "";
            if (listBox1.SelectedItem.ToString() == "Джип") car_type = "Jeep";
            else if (listBox1.SelectedItem.ToString() == "Кабрио") car_type = "Cabrio";
            else if (listBox1.SelectedItem.ToString() == "Комби") car_type = "Combi";
            else if (listBox1.SelectedItem.ToString() == "Седан") car_type = "Sedan";
            else if (listBox1.SelectedItem.ToString() == "Купе") car_type = "Coupe";
            else if (listBox1.SelectedItem.ToString() == "Хечбек") car_type = "Hatchback";
            else car_type = "Other";




            String engine_type = "";
            if (listBox2.SelectedItem.ToString() == "Дизел") engine_type = "Diesel";
            else if (listBox2.SelectedItem.ToString() == "Бензин") engine_type = "Petrol";




            String transmition = "";
            if (listBox3.SelectedItem.ToString() == "Атоматична") transmition = "Automatic";
            else if (listBox3.SelectedItem.ToString() == "Ръчна") transmition = "Manuel";
            else transmition = "Other";


            
            
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();
            string s = "select max(id) as Id from [dbo].[Table]";
            SqlCommand cmd2 = new SqlCommand(s, con);
            SqlDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            int i = Convert.ToInt16(dr["Id"].ToString());
            con.Close();


            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT into [dbo].[Table](Id,CarName,Model,Title,Money,Image,Type,Engine,Transmition,KM,State,RealiseDate,HP,Color,Owner,Info,Residence,AUTOBG,MOBILEBG,CARSBG) Values ('" + (i + 1).ToString() + "',N'" + textBox2.Text + "',N'" + textBox3.Text + "',N'" + textBox4.Text + "',N'" + textBox5.Text + "',@Pic, N'" + car_type + "',N'" + engine_type + "',N'" + transmition + "',N'" + textBox6.Text + "',N'" + textBox7.Text + "',N'" + textBox8.Text + "',N'" + textBox9.Text + "',N'" + textBox10.Text + "',N'" + textBox11.Text + "',N'" + richTextBox1.Text + "',N'" + textBox12.Text + "',N'" + textBox13.Text + "',N'" + textBox14.Text + "',N'" + textBox15.Text + "')", con);
                SqlParameter prm = new SqlParameter("@Pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                cmd.Parameters.Add(prm);
                cmd.ExecuteNonQuery();
                con.Close();
                

                /*Car added = new Car((i + 1), textBox2.Text, textBox3.Text, textBox4.Text, Convert.ToInt32(textBox5.Text),
                    image,
                    car_type, engine_type, transmition, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text,
                    textBox10.Text, Convert.ToInt32(textBox11.Text), richTextBox1.Text, textBox12.Text, textBox13.Text,
                    textBox14.Text, textBox15.Text);
                added.Add_Data_DataBase();*/


                
                // CLEAR THEM ALL
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                richTextBox1.Text = "";

                MessageBox.Show("Обявата беше успешно добавена!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Owners mm = new Owners();
            mm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manuel_AD mm = new Manuel_AD();
            mm.Show();
            this.Hide();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin_Choose mm = new Admin_Choose();
            mm.Show();
        }

        
    }
}
