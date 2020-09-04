using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql;
using System.IO;

namespace ImageEditor

{


    public partial class Form1 : Form
  {
        
        Image image;
        bool k;
        Bitmap bmp;
 
        Bitmap Colorset(Image image, float red, float red1, float red2, float green, float green1, float green2, float blue, float blue1, float blue2, float brigheens)
        {
            Bitmap newi = new Bitmap(image);
            float gamma = 1.0f; // no change in gamma

            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
        new float[] {red, red1,red2, 0, 0}, // scale red
        new float[] { green, green1, green2, 0, 0}, // scale green
        new float[] {blue, blue1, blue2, 0, 0}, // scale blue
        new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
        new float[] { brigheens, brigheens, brigheens, brigheens, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(newi);
            g.DrawImage(newi, new Rectangle(0, 0, newi.Width, newi.Height)
                , 0, 0, newi.Width, newi.Height,
                GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            return newi;

        }




        public Form1()

        {


            InitializeComponent();
            ToolStripMenuItem login = new ToolStripMenuItem("Выход");
            ToolStripMenuItem user_info1 = new ToolStripMenuItem("Информация про пользователя");
            user_info1.Click += Userinfof;
            ToolStripMenuItem Vuhod = new ToolStripMenuItem("Закрыть программу");
            ToolStripMenuItem Vuhod1 = new ToolStripMenuItem("Выйти из аккаунта");
            ToolStripMenuItem Checkimage_onserver = new ToolStripMenuItem("Посмотреть на сервере");
            Checkimage_onserver.Click += checkimage;
            ToolStripMenuItem Checkimageq = new ToolStripMenuItem("Сервер");
            Checkimageq.DropDownItems.Add(Checkimage_onserver);
            Checkimageq.DropDownItems.Add(user_info1);
            Vuhod1.Click += clickq;
            Vuhod.Click += clickq1;
            login.DropDownItems.Add(Vuhod);
            login.DropDownItems.Add(Vuhod1);
            
            menuStrip1.Items.Add(login);
            menuStrip1.Items.Add(Checkimageq);
            

        }
        void checkimage(object sender, EventArgs e)
        {
            this.Close();
            Checkimageonserver a = new Checkimageonserver();
            a.Show();

        }
        void Userinfof(object sender, EventArgs e)
        {
            this.Close();
            Userinfo a = new Userinfo();
            a.Show();

        }
        void clickq(object sender, EventArgs e)
        {
            this.Close();
            login k = new login();
            k.Show();
        }
        void clickq1(object sender, EventArgs e)
        {
            this.Close();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);


                Picture.Image = bmp;
                image = Picture.Image;
               



            }
          
            pictureBox6.Image = Colorset(Picture.Image, 0.5f, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            /////////////////////////////////////////////

            pictureBox8.Image = Colorset(Picture.Image, 0, 0, 0, 0, 0, 0, 0.1f, 0.3f, 0.5f, 0);
            /////////////////////////////

            pictureBox5.Image = Colorset(Picture.Image, .3f, .3f, .3f, .59f, .59f, .59f, .11f, .11f, .11f, 0);
            ///////////////////////////////////////////////////////    

            yellow.Image = Colorset(Picture.Image, 0.4f, 0.4f, 0.8f, 0.59f, 0.59f, 0.59f, 0.11f, 0.11f, 0.11f, 0);
            Theseptone.Image = Colorset(Picture.Image, .393f, .349f, .272f, .769f, .686f, .534f, .189f, .168f, .131f, 0);
            negative.Image = Colorset(Picture.Image, -1, 0, 0, 0, -1, 0, 0, 0, -1, 1);


            




        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (Picture.Image != null)
            {


                if (save.ShowDialog() == DialogResult.OK)
                {
                    Picture.Image.Save(save.FileName);
                   

                }

            }

        }




        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
                bmp = new Bitmap(openFileDialog1.FileName);

                label1.Text = trackBar1.Value.ToString();
                float contrast = trackBar2.Value / 100f; // 
                float gamma = 1.0f; // no change in gamma
                float transparency = trackBar4.Value / 100f;
                float adjustedBrightness = trackBar1.Value / 100f;
                // create matrix that will brighten and contrast the image
                float[][] ptsArray ={
        new float[] {contrast, 0, 0, 0, 0}, // scale red
        new float[] {0, contrast, 0, 0, 0}, // scale green
        new float[] {0, 0, contrast, 0, 0}, // scale blue
        new float[] {0, 0, 0, transparency, 0}, // don't scale alpha
        new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.ClearColorMatrix();
                imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height)
                    , 0, 0, bmp.Width, bmp.Height,
                    GraphicsUnit.Pixel, imageAttributes);
                Picture.Image = bmp;

           


        }





        private void pictureBox2_Click(object sender, EventArgs e)
        { if (Picture.Image != null)
            {
                if (trackBar2.Visible == false)
                {
                    trackBar2.Visible = true;
                    label2.Visible = true;
                }
                else
                {
                    trackBar2.Visible = false;
                    label2.Visible = false;
                }
            }
            else MessageBox.Show("Select image");
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            bmp = new Bitmap(openFileDialog1.FileName);


            label2.Text = trackBar2.Value.ToString();
            float contrast = trackBar2.Value / 100f; // 
            float gamma = 1.0f; // no change in gamma
            float transparency = trackBar4.Value / 100f;
            float adjustedBrightness = trackBar1.Value / 100f;
            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
        new float[] {contrast, 0, 0, 0, 0}, // scale red
        new float[] {0, contrast, 0, 0, 0}, // scale green
        new float[] {0, 0, contrast, 0, 0}, // scale blue
        new float[] {0, 0, 0, transparency, 0}, // don't scale alpha
        new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height)
                , 0, 0, bmp.Width, bmp.Height,
                GraphicsUnit.Pixel, imageAttributes);
            Picture.Image = bmp;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            button4.Text = "Dispose";
            button3.Text = "Save on server";
            this.Text = "ImageEditor";
            label3.Text = "Brightness";
            label4.Text = "Contrast";
            label6.Text = "Red";
            label5.Text = "grayscale";
            label7.Text = "Filters";
            label8.Text = "Blue";
            label9.Text = "Purple";
            TheSepiaTone.Text = "TheSepiaTone";
            negative1.Text = "Negative";
            ///////////////////
            SolidBrush brush = new SolidBrush(Color.Red);
            trackBar3.Visible = false;
            trackBar5.Visible = false;
            trackBar4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            Theseptone.Visible = false;
            negative.Visible = false;
            yellow.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            TheSepiaTone.Visible = false;
            negative1.Visible = false;
        
          
            k = true;
            

        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (Picture.Image != null)
            {
                if (trackBar1.Visible == false)
                {
                    trackBar1.Visible = true;
                    label1.Visible = true;
                }
                else
                {
                    trackBar1.Visible = false;
                    label1.Visible = false;
                }
            }
            else MessageBox.Show("Select image");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (trackBar1.Visible == true) trackBar1.Visible = false;
            if (trackBar2.Visible == true) trackBar2.Visible = false;

            if (pictureBox1.Visible == true & pictureBox2.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
            }
            if (label3.Visible == true & label4.Visible == true)
            {
                label3.Visible = false;
                label4.Visible = false;
            }
            else
            {
                label3.Visible = true;
                label4.Visible = true;
            }
            if (pictureBox4.Visible == true & label7.Visible == true)
            {
                pictureBox4.Visible = false;
                label7.Visible = false;
            }
            else
            {
                pictureBox4.Visible = true;
                label7.Visible = true;
            }
            if(pictureBox7.Visible==true & pictureBox9.Visible == true & comboBox1.Visible == true&pictureBox10.Visible==true)
            {
                pictureBox7.Visible = false;
                pictureBox9.Visible = false;
                comboBox1.Visible = false;
                pictureBox10.Visible = false;

            }
            else
            {
                pictureBox10.Visible = true;
                pictureBox7.Visible = true;
                pictureBox9.Visible = true;
                comboBox1.Visible = true;

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)

        {


            Picture.Image = pictureBox6.Image;


        }



        private void pictureBox4_Click(object sender, EventArgs e)

        {
            if (Picture.Image != null)
            {


                

                if (k == true)
                {
                    pictureBox4.Image = ImageEditor.Properties.Resources.newfiltres;
                    k = false;
                }
                else
                {
                    pictureBox4.Image = ImageEditor.Properties.Resources.filtres;
                    k = true;
                    
                }

                if (pictureBox5.Visible == true & pictureBox6.Visible == true & pictureBox8.Visible == true & yellow.Visible == true & Theseptone.Visible == true & negative.Visible == true)
                {
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    pictureBox8.Visible = false;
                    yellow.Visible = false;
                    Theseptone.Visible = false;
                    negative.Visible = false;
                }
                else
                {
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                    pictureBox8.Visible = true;
                    yellow.Visible = true;
                    Theseptone.Visible = true;
                    negative.Visible = true;
                }
                if (label5.Visible == true & label6.Visible == true & label8.Visible == true & label9.Visible == true & TheSepiaTone.Visible == true & negative1.Visible == true)
                {
                    label5.Visible = false;
                    label6.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                    TheSepiaTone.Visible = false;
                    negative1.Visible = false;
                }
                else
                {
                    label5.Visible = true;
                    label6.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    TheSepiaTone.Visible = true;
                    negative1.Visible = true;

                }




               
            

            pictureBox6.Image = Colorset(Picture.Image, 0.5f, 0, 0, 0, 0, 0, 0, 0, 0, 0f);
            /////////////////////////////////////////////

            pictureBox8.Image = Colorset(Picture.Image, 0, 0, 0, 0, 0, 0, 0.1f, 0.3f, 0.5f, 0f);
            /////////////////////////////

            pictureBox5.Image = Colorset(Picture.Image, .3f, .3f, .3f, .59f, .59f, .59f, .11f, .11f, .11f, 0f);
            ///////////////////////////////////////////////////////    

            yellow.Image = Colorset(Picture.Image, 0.3f, 0.3f, 0.3f, 0.59f, 0.59f, 0.79f, 0, 0.11f, 0.11f, 0f);
            Theseptone.Image = Colorset(Picture.Image, .393f, .349f, .272f, .769f, .686f, .534f, .189f, .168f, .131f, 0f);
            negative.Image = Colorset(Picture.Image, -1, 0, 0, 0, -1, 0, 0, 0, -1, 1);
            }
            else MessageBox.Show("Select image");


















        }



        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Picture.Image = pictureBox5.Image;

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Picture.Image != null)
            {
                comboBox1.Focus();
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        Picture.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        Picture.Invalidate();
                        break;
                    case 1:
                        Picture.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        Picture.Invalidate();
                        break;
                    case 2:
                        Picture.Image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        Picture.Invalidate();
                        break;



                }
            }
            else MessageBox.Show("Select image");
            
            

        }

        

    

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Picture.Image = pictureBox8.Image;

        }

        private void yellow_Click(object sender, EventArgs e)
        {
            Picture.Image = yellow.Image;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Picture.Image = Theseptone.Image;

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Picture.Image = negative.Image;
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            if (Picture.Image != null)
            {
                if (trackBar4.Visible == false)
                {
                    trackBar4.Visible = true;
                    label10.Visible = true;
                }
                else
                {
                    trackBar4.Visible = false;
                    label10.Visible = false;
                }
            }

            else MessageBox.Show("Select image");
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            bmp = new Bitmap(openFileDialog1.FileName);

            label10.Text = trackBar4.Value.ToString();
            float contrast = trackBar2.Value / 100f; // 
            float gamma = 1.0f; // no change in gamma
            float transparency = trackBar4.Value/100f;

            float adjustedBrightness = trackBar1.Value / 100f;
            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
        new float[] {contrast, 0, 0, 0, 0}, // scale red
        new float[] {0, contrast, 0, 0, 0}, // scale green
        new float[] {0, 0, contrast, 0, 0}, // scale blue
        new float[] { transparency, transparency, transparency, transparency, transparency}, // don't scale alpha
        new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height)
                , 0, 0, bmp.Width, bmp.Height,
                GraphicsUnit.Pixel, imageAttributes);
            Picture.Image = bmp;
        }

       
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            
       
            Picture.Size = new Size(trackBar5.Value, Picture.Size.Height);
            Picture.Left = (this.ClientSize.Width - Picture.Width) / 2;
            Picture.Top = (this.ClientSize.Height - Picture.Height) / 2;






        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Picture.Size = new Size(Picture.Size.Width, trackBar3.Value);
            Picture.Left = (this.ClientSize.Width - Picture.Width) / 2;
            Picture.Top = (this.ClientSize.Height - Picture.Height) / 2;

        }

        private void Picture_Resize(object sender, EventArgs e)
        {
            
            Picture.Left = (this.ClientSize.Width - Picture.Width) / 2;
            Picture.Top = (this.ClientSize.Height - Picture.Height) / 2;
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)

        {
            if (Picture.Image != null)
            {
                if (trackBar5.Visible == true & trackBar3.Visible == true)
                {
                    trackBar3.Visible = false;
                    trackBar5.Visible = false;

                }
                else
                {
                    trackBar5.Visible = true;
                    trackBar3.Visible = true;
                }
            }
            else MessageBox.Show("Select Image");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Picture.Image != null)
            {
                MySqlConnection k1 = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
                string[] abs = login.ksf.Split(' ');
                k1.Open();
                MySqlCommand mp1 = new MySqlCommand("select id from users21.user_info where Login like'" + abs[0] + "'AND password like'" + abs[1] + "';", k1);
                MySqlDataReader c1 = mp1.ExecuteReader();
                int a = 0;


                if (c1.HasRows)
                {
                    if (c1.Read())
                    {
                        a = c1.GetInt32(0);


                    }


                }

                c1.Close();
                k1.Close();
                k1.Dispose();
                mp1.Dispose();
                MySqlConnection kf = new MySqlConnection("server= localhost ;UserId= Bruce ;database= users21 ;password= givanchy ;");
                kf.Open();
                MySqlCommand mpc = new MySqlCommand("INSERT INTO users21.users_image(id,Image,image_size,name_image)" + "VALUES(@id,@image,@image_s,@name_im)", kf);
                MemoryStream kfg = new MemoryStream();
                Picture.Image.Save(kfg, System.Drawing.Imaging.ImageFormat.Png);
                byte[] ima = kfg.ToArray();
                mpc.Parameters.AddWithValue("@id", a);
                mpc.Parameters.AddWithValue("@image", ima);
                mpc.Parameters.AddWithValue("@image_s", ima.Length);
                mpc.Parameters.AddWithValue("@name_im", Path.GetFileNameWithoutExtension(openFileDialog1.FileName));
                int b = mpc.ExecuteNonQuery();
                kf.Close();
                kf.Dispose();
                mpc.Dispose();
            }
            else MessageBox.Show("Select image");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Picture.Dispose();
        }

       
    }

}



