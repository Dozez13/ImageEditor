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

namespace ImageEditor
{
    public partial class login : Form
    {
        public static string ksf;
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            
            label1.Text = "";
            label2.Text = "Логин";
            label3.Text = "Пароль";
            button1.Text = "Войти";
            button2.Text = "Регистрация";
            
            textBox2.PasswordChar = '*';
            
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                MySqlConnection k = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
                k.Open();
                MySqlCommand mp = new MySqlCommand("select password from users21.user_info where Login like'" + textBox1.Text + "';", k);
                MySqlDataReader c = mp.ExecuteReader();
                string a = "";

                if (c.HasRows)
                {
                    if (c.Read())
                    {
                        a = c.GetString(0);


                    }


                }
                else
                {
                    label1.Text = "Неправильный логин или пароль";
                    c.Close();
                    mp.Dispose();
                    k.Close();
                    k.Dispose();
                    return;
                }
                if (a == textBox2.Text)
                {
                    
                    ksf = textBox1.Text + " " + textBox2.Text;
                    c.Close();
                    mp.Dispose();
                    k.Close();
                    k.Dispose();
                    this.Visible = false;
                    Form1 ks = new Form1();
                    ks.Show();




                }
              

            }
            else label1.Text = "Введите логин";
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            registry a = new registry();
            a.Show();
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, ((Label)sender).Width - 1, ((Label)sender).Height - 1));

        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, ((Label)sender).Width - 1, ((Label)sender).Height - 1));
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, ((Label)sender).Width - 1, ((Label)sender).Height - 1));
        }
       
    }
}
