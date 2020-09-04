using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql;
using System.IO;

namespace ImageEditor
{   
    public partial class Checkimageonserver : Form
    {
        public static Image[]absd;
        public Checkimageonserver()
        {
            InitializeComponent();
            
        }
        
        public int checkid()
        {
            MySqlConnection k1 = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
            string[] abs = login.ksf.Split(' ');
            k1.Open();
            MySqlCommand mp1 = new MySqlCommand("select id from users21.user_info where Login like'" + abs[0] + "'AND password like'" + abs[1] + "';", k1);
            MySqlDataReader c = mp1.ExecuteReader();
            int a = 0;
            if (c.HasRows)
            {
                if (c.Read())
                {
                    a = c.GetInt32(0);



                }


            }

            
            c.Close();
            k1.Close();
            k1.Dispose();
            mp1.Dispose();
            return a;

        }
        public int fill()
        {
            MySqlConnection kf1 = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
            kf1.Open();
            MySqlCommand mpc1 = new MySqlCommand("select name_image from users21.users_image where id=" + Convert.ToString(checkid()) + ";", kf1);
            MySqlDataReader c3 = mpc1.ExecuteReader();
            int i = 0;
            while (c3.Read())
            {

                comboBox1.Items.Add(c3.GetString(0));
                i++;
            }
            kf1.Close();
            kf1.Dispose();
            mpc1.Dispose();
            c3.Close();
            return i;

        }
        private void Checkimageonserver_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.Name, label1.Font.Size + 4, label1.Font.Style, label1.Font.Unit);
            label1.Text = "Выберите имя изображения на сервере";
            pictureBox1.Image = new Bitmap(ImageEditor.Properties.Resources.insert);
            
            comboBox1.MaxDropDownItems = fill();
            absd=image();
           
            


        }
        public  Image[] image()
        {
            byte[] rawData;
            MySqlConnection kf = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
            kf.Open();
            MySqlCommand mpc = new MySqlCommand("select Image,image_size from users21.users_image where id=" + Convert.ToString(checkid()) + ";", kf);
            MySqlDataReader c2 = mpc.ExecuteReader();
            Image[] a = new Image[comboBox1.MaxDropDownItems];
            int i = 0;
            while (c2.Read())
            {
                rawData = new byte[c2.GetInt32(1)];
                c2.GetBytes(c2.GetOrdinal("Image"), 0, rawData, 0, c2.GetInt32(1));
                a[i] = bytearraytoimage(rawData);
                i++;
            }


            kf.Close();
            kf.Dispose();
            mpc.Dispose();
            c2.Close();
            return a;
        
            

        }
        public Image bytearraytoimage(byte[] a)
        {
            MemoryStream aq = new MemoryStream(a);
            return Image.FromStream(aq);

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = absd[comboBox1.SelectedIndex];




        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, ((Label)sender).Width - 1, ((Label)sender).Height - 1));
        }
    }
}
