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
    public partial class Client_Area : Form
    {
        private List<Car> CarList = new List<Car>();
        private List<Owner> OwnerList = new List<Owner>();

        
        Form mainFormHandler;

        public Client_Area()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // Действия още при зареждане на формата..
        {
            try
            {
                this.tableTableAdapter.Fill(this.database1DataSet.Table); // изобразява таблицата
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


            AdsFill(); // правим обекти и ги зареждаме с информацията от базата данни!
            OwnersFill();

            UpdateOwnerInfo(); // при зареждане ъпдейтва информацията за OWNER-a

            mainFormHandler = Application.OpenForms[1]; // 0 -1ва форма      1 - 2ра форма
            mainFormHandler.Size = new Size(555, 970);

            button8.Select(); // селектваме автоматично при LOAD да е на търсчаката

            moneyLabel2.Text = "Цена: " + moneyLabel2.Text + " лева"; // правим цената да излгежда по-добре (най-отгоре в обявата)

            listBox1.SelectedIndex = 0; // автоматично слагаме на листбокса за двигател да избере 1 (че да няма някой объркал се - наизбрал)
            listBox2.SelectedIndex = 0; // същото слагаме и за листбокса за година 1 (OT)
            listBox5.SelectedIndex = 18; // същото сллгаме и за листбокса за година 2 (ДО)
            listBox3.SelectedIndex = 0; // същото слагаме и за листбокса за тип кола
            listBox4.SelectedIndex = 0; // същото слагаме и за листбокса на скоростна кутия

            JustUpdate();
        }

        private void button3_Click(object sender, EventArgs e) // бутон Напред
        {
            tableBindingSource.MoveNext(); // 1 напред

            UpdateOwnerInfo(); // ъпдейт на инфо-то за OWNER-a

            moneyLabel2.Text = "";
            moneyLabel2.Text = "Цена: " + moneyTextBox1.Text + " лева";
        }

        private void button2_Click(object sender, EventArgs e) // бутон Назад
        {
            tableBindingSource.MovePrevious(); // 1 назад

            UpdateOwnerInfo(); // ъпдейт на инфо-то за OWNER-a

            moneyLabel2.Text = "";
            moneyLabel2.Text = "Цена: " + moneyTextBox1.Text + " лева";
        }

        bool search = false;
        private void button4_Click(object sender, EventArgs e) // search button !!!
        {
            try
            {
                SqlConnection con = new SqlConnection(Program.source);
                con.Open();

                DataTable dt = new DataTable();

                String Select = "SELECT * FROM [dbo].[Table] WHERE ";
                String Filter1 = "";
                String Filter2 = "";
                String Filter3 = "";
                String Filter4 = "";
                String Filter5 = "";
                String Filter6 = "";
                String Filter7 = "";

                if (CarName.Checked)
                {
                    if (textBox1.Text.Length > 0)
                    {
                        Filter1 = "CarName = '" + textBox1.Text + "' ";
                    }
                    else
                    {
                        MessageBox.Show("Грешно въведени данни при полето Марка !");
                        return;
                    }
                }
                else Filter1 = "";

                if (Model.Checked)
                {
                    if (textBox5.Text.Length > 0)
                    {
                        if (CarName.Checked) Filter2 = "AND Model = '" + textBox5.Text + "' ";
                        else Filter2 = "Model = '" + textBox5.Text + "' ";
                    }
                    else
                    {
                        MessageBox.Show("Грешно въведени данни при полето Модел !");
                        return;
                    }
                }
                else Filter2 = "";

                if (Money.Checked)
                {
                    bool first = false;
                    if (textBox4.Text.Length > 0)
                    {
                        if (CarName.Checked || Model.Checked) Filter3 = "AND Money >= '" + int.Parse(textBox4.Text) + "' ";
                        else Filter3 = "Money >= '" + int.Parse(textBox4.Text) + "' ";

                        first = true;
                    }

                    if (textBox6.Text.Length > 0)
                    {
                        if(first) Filter3 += "AND Money <= '" + int.Parse(textBox6.Text) + "' ";
                        else Filter3 += "Money <= '" + int.Parse(textBox6.Text) + "' ";
                    }

                    if(textBox4.Text.Length < 1 && textBox6.Text.Length < 1)
                    {
                        MessageBox.Show("Грешно въведени данни при полето Цена !");
                        return;
                    }
                }
                else Filter3 = "";

                if (Engine.Checked)
                {
                    String engine_type = "";
                    if (listBox1.SelectedItem.ToString() == "Дизел") engine_type = "Diesel";
                    else if (listBox1.SelectedItem.ToString() == "Бензин") engine_type = "Petrol";

                    if (CarName.Checked || Model.Checked || Money.Checked) Filter4 = "AND Engine = '" + engine_type + "' ";
                    else Filter4 = "Engine = '" + engine_type + "' ";
                }
                else Filter4 = "";


                if (Date.Checked)
                {
                    if (CarName.Checked || Model.Checked || Money.Checked || Engine.Checked)
                        Filter5 = "AND RealiseDate >= '" + listBox2.SelectedItem.ToString() + "' AND RealiseDate <= '" + listBox5.SelectedItem.ToString() + "' ";
                    else
                        Filter5 = "RealiseDate >= '" + int.Parse(listBox2.SelectedItem.ToString()) + "' AND RealiseDate <= '" + listBox5.SelectedItem.ToString() + "' ";
                }
                else Filter5 = "";


                if (Type.Checked)
                {
                    String car_type = "";
                    if (listBox3.SelectedItem.ToString() == "Джип") car_type = "Jeep";
                    else if (listBox3.SelectedItem.ToString() == "Кабрио") car_type = "Cabrio";
                    else if (listBox3.SelectedItem.ToString() == "Комби") car_type = "Combi";
                    else if (listBox3.SelectedItem.ToString() == "Седан") car_type = "Sedan";
                    else if (listBox3.SelectedItem.ToString() == "Купе") car_type = "Coupe";
                    else if (listBox3.SelectedItem.ToString() == "Хечбек") car_type = "Hatchback";
                    else car_type = "Other";

                    /*
                    
                    Джип
                    Кабрио
                    Комби
                    Седан
                    Купе
                    Хечбек

                    */

                    if (CarName.Checked || Model.Checked || Money.Checked || Engine.Checked || Date.Checked) Filter6 = "AND Type = '" + car_type + "' ";
                    else Filter6 = "Type = '" + car_type + "' ";
                }
                else Filter6 = "";

                if (Transmition.Checked)
                {
                    String transmition = "";
                    if (listBox4.SelectedItem.ToString() == "Атоматична") transmition = "Automatic";
                    else if (listBox4.SelectedItem.ToString() == "Ръчна") transmition = "Manuel";
                    else transmition = "Other";


                    //Атоматична
                    //Ръчна

                    if (CarName.Checked || Model.Checked || Money.Checked || Engine.Checked || Date.Checked || Type.Checked) Filter7 = "AND Transmition = '" + transmition + "' ";
                    else Filter7 = "Transmition = '" + transmition + "' ";
                }
                else Filter7 = "";


                if (!CarName.Checked && !Model.Checked && !Money.Checked && !Engine.Checked && !Date.Checked && !Type.Checked && !Transmition.Checked)
                {
                    MessageBox.Show("Вие не сте избрали конректни критерии за търсене!\nОбявите бяха презаредени Успешно!", "Информационно табло!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Select = "SELECT * FROM [dbo].[Table]";
                }
                
                String Final = Select + Filter1 + Filter2 + Filter3 + Filter4 + Filter5 + Filter6 + Filter7;
                SqlDataAdapter sda = new SqlDataAdapter(Final, con);
                sda.Fill(dt);

                tableDataGridView.DataSource = dt;
                search = true;

                con.Close();

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void JustUpdate()
        {
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();

            DataTable dt = new DataTable();
            String Select = "SELECT * FROM [dbo].[Table]";
            SqlDataAdapter sda = new SqlDataAdapter(Select, con);
            sda.Fill(dt);

            tableDataGridView.DataSource = dt;

            con.Close();
        }

     
        private void button8_Click(object sender, EventArgs e) // бутона за търсчаката (SHOW/HIDE)
        {
            bool show = false;

            if (CarName.Visible == true) CarName.Visible = false;
            else CarName.Visible = true;

            if (Model.Visible == true) Model.Visible = false;
            else Model.Visible = true;

            if (Money.Visible == true) Money.Visible = false;
            else Money.Visible = true;

            if (Engine.Visible == true) Engine.Visible = false;
            else Engine.Visible = true;

            if (Date.Visible == true) Date.Visible = false;
            else Date.Visible = true;

            if (Type.Visible == true) Type.Visible = false;
            else Type.Visible = true;

            if (Transmition.Visible == true) Transmition.Visible = false;
            else Transmition.Visible = true;

            if (label5.Visible == true) label5.Visible = false;
            else label5.Visible = true;

            if (label6.Visible == true) label6.Visible = false;
            else label6.Visible = true;


            if (button4.Visible == true) button4.Visible = false;
            else button4.Visible = true;


            if (tableDataGridView.Visible == true) tableDataGridView.Visible = false;
            else tableDataGridView.Visible = true;


            if(show)
            {
                mainFormHandler.Size = new Size(555, 970);
                show = false;
            }
            else
            {
                mainFormHandler.Size = new Size(1300, 970);
                show = true;
            }
        }



        private void Money_CheckedChanged(object sender, EventArgs e) // когато натиснем чекбокса на мъни, правим висибле/инвисибле текстбокса
        {
            if (textBox4.Visible == true) textBox4.Visible = false;
            else textBox4.Visible = true;

            if (textBox6.Visible == true) textBox6.Visible = false;
            else textBox6.Visible = true;

            if (label8.Visible == true) label8.Visible = false;
            else label8.Visible = true;
        }

        private void CarName_CheckedChanged(object sender, EventArgs e) // ...
        {
            if (textBox1.Visible == true) textBox1.Visible = false;
            else textBox1.Visible = true;
        }

        private void Model_CheckedChanged_1(object sender, EventArgs e) // ...
        {
            if (textBox5.Visible == true) textBox5.Visible = false;
            else textBox5.Visible = true;
        }

        private void Engine_CheckedChanged(object sender, EventArgs e) // ...
        {
            if (listBox1.Visible == true) listBox1.Visible = false;
            else listBox1.Visible = true;
        }

        private void Date_CheckedChanged(object sender, EventArgs e) // ...
        {
            if (listBox2.Visible == true) listBox2.Visible = false;
            else listBox2.Visible = true;

            if (listBox5.Visible == true) listBox5.Visible = false;
            else listBox5.Visible = true;

            if (label9.Visible == true) label9.Visible = false;
            else label9.Visible = true;
        }

        private void Type_CheckedChanged(object sender, EventArgs e) // ...
        {
            if (listBox3.Visible == true) listBox3.Visible = false;
            else listBox3.Visible = true;
        }

        private void Transmition_CheckedChanged(object sender, EventArgs e) // ...
        {
            if (listBox4.Visible == true) listBox4.Visible = false;
            else listBox4.Visible = true;
        }

       



        private void button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(aUTOBGTextBox.Text); // отваря линк в deafult браузъра
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(mOBILEBGTextBox.Text); // отваря линк в deafult браузъра
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(cARSBGTextBox.Text); // отваря линк в deafult браузъра
        }


        int ID;
        private void tableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateOwnerInfo(); // обновяваме информацията за собствнеика
            moneyLabel2.Text = "";
            moneyLabel2.Text = "Цена: " + moneyTextBox1.Text + " лева";

            if (!search) return; // ако не е ползвана търсачката, нека не правим нищо !

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tableDataGridView.Rows[e.RowIndex];
                ID = Convert.ToInt32(row.Cells["Id"].Value.ToString());
            }

            UpdateLeftPannel(ID);
        }


        

        public void AdsFill()
        {
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();

            SqlDataAdapter info = new SqlDataAdapter("Select * From [dbo].[Table]", con);
            DataTable dt = new DataTable();
            info.Fill(dt);

            int Id;
            string CarName;
            string Model;
            string Title;
            int Money;
            byte[] Image;
            string Type;
            string Engine;
            string Transmition;
            string KM;
            string State;
            string RD;
            string HP;
            string Color;
            int Owner;
            string Info;
            string Residence;
            string Autobg;
            string Mobilebg;
            string Carsbg;

            foreach (DataRow row in dt.Rows)
            {
                Id = (Convert.ToInt32(row["Id"]));
                CarName = (row["CarName"].ToString());
                Model = (row["Model"].ToString());

                Title = (row["Title"].ToString());
                Money = (Convert.ToInt32(row["Money"]));

                Image = System.Text.Encoding.Unicode.GetBytes(row["Image"].ToString());

                Type = (row["Type"].ToString());
                Engine = (row["Engine"].ToString());
                Transmition = (row["Transmition"].ToString());
                KM = (row["KM"].ToString());
                State = (row["State"].ToString());
                RD = (row["RealiseDate"].ToString());
                HP = (row["HP"].ToString());
                Color = (row["Color"].ToString());
                Owner = (Convert.ToInt32(row["Owner"]));
                Info = (row["Info"].ToString());
                Residence = (row["Residence"].ToString());
                Autobg = (row["Autobg"].ToString());
                Mobilebg = (row["Mobilebg"].ToString());
                Carsbg = (row["Carsbg"].ToString());

                CarList.Add(new Car(Id, CarName, Model, Title, Money, Image, Type, Engine, Transmition, KM, State, RD, HP, Color, Owner, Info, Residence, Autobg, Mobilebg, Carsbg));
            }
            con.Close();
        }



        public void OwnersFill()
        {
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();

            SqlDataAdapter info = new SqlDataAdapter("Select * From [dbo].[CarOwner]", con);
            DataTable dt = new DataTable();
            info.Fill(dt);

            int Id;
            string Name;
            string Phone;

            foreach (DataRow row in dt.Rows)
            {
                Id = (Convert.ToInt32(row["Id"]));
                Name = (row["Name"].ToString());
                Phone = (row["Phone"].ToString());

                OwnerList.Add(new Owner(Id, Name, Phone));
            }
            con.Close();
        }



        public void UpdateLeftPannel(int IDd) // нужно ни е ъпдейтване на информацията за левия панел след използване на търсачката..
        {
            foreach (var a in CarList)
            {
                if (a.ID == IDd) // ако object.ID == подадения като параметър ID
                {
                    idLabel3.Text = a.ID.ToString();                // (row["Id"].ToString());
                    titleLabel1.Text = a.Title;                     // (row["Title"].ToString());
                    moneyTextBox1.Text = a.Money.ToString();        // (row["Money"].ToString());
                    typeTextBox.Text = a.Type;                      // (row["Type"].ToString());
                    engineTextBox.Text = a.Engine;                  // (row["Engine"].ToString());
                    transmitionTextBox.Text = a.Transmition;        // (row["Transmition"].ToString());
                    kMTextBox.Text = a.Km;                          // (row["KM"].ToString());
                    stateTextBox.Text = a.State;                    // (row["State"].ToString());
                    realiseDateTextBox.Text = a.Rd;                 // (row["RealiseDate"].ToString());
                    hPTextBox.Text = a.Hp;                          // (row["HP"].ToString());
                    colorTextBox.Text = a.Color;                    // (row["Color"].ToString());
                    residenceTextBox.Text = a.Residence;            // (row["Residence"].ToString());
                    infoTextBox.Text = a.Info;                      // (row["Info"].ToString());
                    aUTOBGTextBox.Text = a.Autobg;                  // (row["AUTOBG"].ToString());
                    mOBILEBGTextBox.Text = a.Mobilebg;              // (row["MOBILEBG"].ToString());
                    cARSBGTextBox.Text = a.Carsbg;                  // (row["CARSBG"].ToString());

                    moneyLabel2.Text = "";
                    moneyLabel2.Text = "Цена: " + moneyTextBox1.Text + " лева";

                    ownerLabel1.Text = a.Owner.ToString();          //(row["Owner"].ToString());

                    UpdateImage(IDd);
                }
            }
        }

        public void UpdateOwnerInfo() // нужно ни е ъпдейтване на информацията за OWNER-а от време на време
        {
            foreach (var a in OwnerList)
            {
                if (a.ID == (Convert.ToInt32(ownerLabel1.Text)))
                {
                    nameTextBox.Text = a.Name;
                    phoneTextBox1.Text = a.Phone;
                }
            }
        }

        public void UpdateImage(int IDD) // лоадване на снимка от подадено ID
        {
            SqlConnection con = new SqlConnection(Program.source);
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * From [dbo].[Table] WHERE Id = '" + IDD + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            byte[] MyData = new Byte[0];
            MyData = (byte[])dt.Rows[0][5];
            MemoryStream str = new MemoryStream(MyData);
            imagePictureBox.Image = Image.FromStream(str);

            con.Close();
        }

        /*public void UpdateImage(int IDD) // лоадване на снимка от подадено ID
        {
            MemoryStream str = new MemoryStream();
            {
                foreach (var a in CarList)
                {
                    if (a.ID == IDD) // ако object.ID == подадения като параметър ID
                    {
                        str.Write(a.Image, 0, a.Image.Length);
                        imagePictureBox.Image = Image.FromStream(str);
                    }
                }
            }
        }*/
    }
}
