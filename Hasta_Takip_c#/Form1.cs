using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using MySql.Data.MySqlClient;

namespace Hasta_Takip
{
    public partial class Form1 : Form
    {
        private string data;
        public List<string> veriler = new List<string>();
        public string[] dot,dotx;
        public string tarih;
        public string room1 = "10";
        public string room2 = "40";
        public string pulse1 = "70";
        public string pulse2 = "120";
        public string ates1 = "15";
        public string ates2 = "45";
        public bool read_bool = true;
        MySqlConnection conn= new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=patient");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_ac.Enabled = false;
            maskedTextBox1.Enabled = false;
            string [] ports=SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);
            serialPort1.DataReceived+=new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
        }
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displayData_event));
        }
        private void displayData_event(object sender, EventArgs e)
        {
            
            veriler.Add(DateTime.Now.ToString() + " " + data);
            listBox1.Items.Insert(0, DateTime.Now.ToString() + "       " + data);
            //veri tabanı insert işilemi
            //CREATE TABLE `hasta`.`veriler` ( `tarih` VARCHAR(25) NOT NULL , `tc` DOUBLE NOT NULL , `nabiz` INT NOT NULL , `ates` INT NOT NULL ) ENGINE = InnoDB;
            //(char)i sayıyı byte'a çevirme
            if (listBox1.Items.Count > 600)
            {
                for(int i = listBox1.Items.Count/2;i< listBox1.Items.Count;i++)
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
            if (veriler.Count > 5)
            {
                try
                {
                    insert(veriler);
                    veriler.Clear();
                }
                catch (Exception)
                {

                }

            }

            //5 gelen veride bir kere sınırları kontrol ediyoruz
            if (veriler.Count % 1 == 0 || read_bool ==true)
            {
                //alarm gönderme
                read_bool = true;
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM limits order by id desc LIMIT 1;", conn);
                try
                {
                    conn.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        room1 = dr.GetString(1);
                        room2 = dr.GetString(2);
                        pulse1 = dr.GetString(3);
                        pulse2 = dr.GetString(4);
                        ates1 = dr.GetString(5);
                        ates2 = dr.GetString(6);
                    }
                    conn.Close();
                    read_bool = false;
                }
                catch (Exception)
                {

                }
                   

            }

            label10.Text = room1;
            label11.Text = room2;
            label12.Text = pulse1;
            label13.Text = pulse2;
            label14.Text = ates1;
            label15.Text = ates2;

            dotx = data.Split(' ');
            label16.Text = dotx[1];//nabız
            label17.Text = dotx[2];//ates
            label4.Text = dotx[3];//oda
            if (Convert.ToInt32(dotx[1]) >Convert.ToInt32(pulse1) && Convert.ToInt32(dotx[1]) < Convert.ToInt32(pulse2) && Convert.ToInt32(dotx[2]) > Convert.ToInt32(ates1) && Convert.ToInt32(dotx[2]) < Convert.ToInt32(ates2) && Convert.ToInt32(dotx[3]) > Convert.ToInt32(room1) && Convert.ToInt32(dotx[3]) < Convert.ToInt32(room2))
            {
                serialPort1.Write("0");
            }
            else
            {
                if(Convert.ToInt32(dotx[1]) > Convert.ToInt32(pulse1) && Convert.ToInt32(dotx[1]) < Convert.ToInt32(pulse2) && Convert.ToInt32(dotx[2]) > Convert.ToInt32(ates1) && Convert.ToInt32(dotx[2]) < Convert.ToInt32(ates2))
                    //bu kısım 2 olacak
                    serialPort1.Write("2");//hemsireye sinyal ver oda sıcaklığı bozuk 
                else
                    serialPort1.Write("1");//hemsire ve doktora sinyal ver =oda sıcaklığıyla beraber ates veya nabız bozuk
            }

        }
        private void insert(List<string> veri)
        {
            //veri.Reverse();
            MySqlCommand cmd;
            string query;
            conn.Open();
            foreach (string s in veri)
            {
                dot = s.Split(' ');
                tarih = dot[0]+" "+ dot[1];//tarih ve saat için
                query = "insert into datas (date, tc, nabiz, ates, sicaklik) values('" + tarih + "',"+maskedTextBox1.Text+" ,"+Convert.ToInt32(dot[3])+ ","+ Convert.ToInt32(dot[4]) + "," + Convert.ToInt32(dot[5]) + ");";
                cmd = new MySqlCommand(query,conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                button2.Enabled = true;
                button1_ac.Enabled = false;
                maskedTextBox1.Enabled = false;
                /*
                int bip = Convert.ToInt32(label4.Text);
                char cm=(char)bip;
                label8.Text = cm.ToString();
                Thread.Sleep(1000);
                serialPort1.Write(cm.ToString());*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            button1_ac.Enabled = true;
            button2.Enabled = false;
            maskedTextBox1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (veriler.Count > 0)
                insert(veriler);
            if (serialPort1.IsOpen) 
                serialPort1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Enabled = true;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (maskedTextBox1.TextLength == 11 && Convert.ToDouble(maskedTextBox1.Text) > 10000000000)
                {
                    button1_ac.Enabled = true;
                }
                else
                {
                    button1_ac.Enabled = false;
                }
            }
            catch (Exception)
            {
                maskedTextBox1.Clear();
            }

        }
    }
}
