using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelizer
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Form1 form;
        public Form3(Form1 f)
        {
            InitializeComponent();
            form = f;
        }
        Bitmap image1;
        Bitmap image2;
        int[] pos = new int[3];
        Bitmap cursor1 = new Bitmap(50,7);
        Bitmap cursor2 = new Bitmap(9,9);
        Point mouse;
        private void Form3_Load(object sender, EventArgs e)
        {
            pos[0] = 0;
            pos[1] = 0;
            pos[2] = 0;
            image1 = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox2.BackColor = Color.FromArgb(255, 255, 255);
            image2 = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            for (int i = 0; i < pictureBox2.Height; i++)
            {
                for (int j = 6; j < pictureBox2.Width - 6; j++)
                {
                    image2.SetPixel(j, i, Color.FromArgb(255 - i, 0, 0));
                }
            }
            for (int i = 0; i < cursor1.Width; i++)
            {
                cursor1.SetPixel(i, 3, Color.Black);
            }
            for(int i = 0; i < cursor1.Height; i++)
            {
                cursor1.SetPixel(0, i, Color.Black);
                cursor1.SetPixel(49, i, Color.Black);
                cursor1.SetPixel(1, i, Color.Black);
                cursor1.SetPixel(48, i, Color.Black);
            }
            for(int i =0;i<9;i++)
            {
                if (i < 3 || i > 5) {
                    cursor2.SetPixel(i,4,Color.Black);
                    cursor2.SetPixel(4, i, Color.Black);
                }
            }
            Bitmap image3 = new Bitmap(image2);
            using (Graphics g = Graphics.FromImage(image3))
            {
                g.DrawImage(image2, 0, 0);
                g.DrawImage(cursor1, 0, -3);
            }
            pictureBox2.Image = image3;
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    image1.SetPixel(i, j, Color.FromArgb(255 , 255-i, 255-j));
                }
            }
            Bitmap image4 = new Bitmap(image1);
            using (Graphics g = Graphics.FromImage(image4))
            {
                g.DrawImage(image1, 0, 0);
                g.DrawImage(cursor2, pos[1] - 4, pos[2] - 4);
            }
            pictureBox1.Image = image4;
            Bitmap result_color = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            using (Graphics g = Graphics.FromImage(result_color))
            {
                g.Clear(Color.FromArgb(255 - pos[0], 255 - pos[1], 255 - pos[2]));
            }
            pictureBox3.Image = result_color;
        }

        bool mouse_pressed = false;
        bool mouse_pressed2 = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            mouse = pictureBox2.PointToClient(MousePosition);
            if (mouse.X>=0&&mouse.X<pictureBox2.Width&& mouse.Y >= 0 && mouse.Y < pictureBox2.Height&&mouse_pressed)
            {
                Bitmap image3 = new Bitmap(image2);
                using (Graphics g = Graphics.FromImage(image3))
                {
                    g.DrawImage(image2, 0, 0);
                    g.DrawImage(cursor1, 0, mouse.Y - 3);
                }
                pictureBox2.Image = image3;
                pos[0] = mouse.Y;
                for (int i = 0; i < pictureBox1.Width; i++)
                {
                    for (int j = 0; j < pictureBox1.Height; j++)
                    {
                        image1.SetPixel(i, j, Color.FromArgb(255 - mouse.Y, 255 - i, 255 - j));
                    }
                }
                Bitmap image4 = new Bitmap(image1);
                using (Graphics g = Graphics.FromImage(image4))
                {
                    g.DrawImage(image1, 0, 0);
                    g.DrawImage(cursor2, pos[1]-4, pos[2] - 4);
                }
                pictureBox1.Image = image4;
                textBox1.Text = (255 - mouse.Y).ToString();
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_pressed = true;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_pressed = false;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            mouse = pictureBox1.PointToClient(MousePosition);
            if (mouse.X >= 0 && mouse.X < pictureBox1.Width && mouse.Y >= 0 && mouse.Y < pictureBox1.Height && mouse_pressed2)
            {
                Bitmap image4 = new Bitmap(image1);
                using (Graphics g = Graphics.FromImage(image4))
                {
                    g.DrawImage(image1, 0, 0);
                    g.DrawImage(cursor2, mouse.X-4, mouse.Y - 4);
                }
                pictureBox1.Image = image4;
                pos[1] = mouse.X;
                textBox2.Text = (255 - mouse.X).ToString();
                pos[2] = mouse.Y;
                textBox3.Text = (255 - mouse.Y).ToString();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_pressed2 = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_pressed2 = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int r = 255 - int.Parse(textBox1.Text);
                Bitmap image3 = new Bitmap(image2);
                using (Graphics g = Graphics.FromImage(image3))
                {
                    g.DrawImage(image2, 0, 0);
                    g.DrawImage(cursor1, 0, r - 3);
                }
                pictureBox2.Image = image3;
                pos[0] = r;
                for (int i = 0; i < pictureBox1.Width; i++)
                {
                    for (int j = 0; j < pictureBox1.Height; j++)
                    {
                        image1.SetPixel(i, j, Color.FromArgb(255 - r, 255 - i, 255 - j));
                    }
                }
                Bitmap image4 = new Bitmap(image1);
                using (Graphics g = Graphics.FromImage(image4))
                {
                    g.DrawImage(image1, 0, 0);
                    g.DrawImage(cursor2, pos[1] - 4, pos[2] - 4);
                }
                pictureBox1.Image = image4;
                Bitmap result_color = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                using (Graphics g = Graphics.FromImage(result_color))
                {
                    g.Clear(Color.FromArgb(255-pos[0],255- pos[1],255- pos[2]));
                }
                pictureBox3.Image = result_color;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                int gr = 255 - int.Parse(textBox2.Text);
                Bitmap image4 = new Bitmap(image1);
                using (Graphics g = Graphics.FromImage(image4))
                {
                    g.DrawImage(image1, 0, 0);
                    g.DrawImage(cursor2, gr - 4, pos[2] - 4);
                }
                pictureBox1.Image = image4;
                pos[1] = gr;
                Bitmap result_color = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                using (Graphics g = Graphics.FromImage(result_color))
                {
                    g.Clear(Color.FromArgb(255-pos[0],255-pos[1], 255-pos[2]));
                }
                pictureBox3.Image = result_color;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                int b = 255 - int.Parse(textBox3.Text);
                Bitmap image4 = new Bitmap(image1);
                using (Graphics g = Graphics.FromImage(image4))
                {
                    g.DrawImage(image1, 0, 0);
                    g.DrawImage(cursor2, pos[1] - 4, b - 4);
                }
                pictureBox1.Image = image4;
                pos[2] = b;
                Bitmap result_color = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                using (Graphics g = Graphics.FromImage(result_color))
                {
                    g.Clear(Color.FromArgb(255-pos[0], 255-pos[1],255-pos[2]));
                }
                pictureBox3.Image = result_color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.dataGridView1.Rows.Add(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text));
            this.Close();
        }
    }
}
