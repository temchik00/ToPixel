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
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public Form1()
        {
            InitializeComponent();
        }
        Image image;
        public Bitmap im;
        private void button1_Click(object sender, EventArgs e)
        {
            string s;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images (*.png, *.jpg, *.jpeg, *.bmp)|*.png; *.jpg; *.jpeg; *.bmp";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                s = dialog.FileName;
                image = Image.FromFile(@s);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "png|*.png";
            if(save.ShowDialog() == DialogResult.OK)
            {
                im.Save(@save.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num_pixels_w, num_pixels_h;
            if(image != null)
            {
                if(int.TryParse(textBox1.Text, out num_pixels_h)&& int.TryParse(textBox2.Text, out num_pixels_w))
                {
                    
                    int w = image.Width;
                    int h = image.Height;
                    Size size_of_image = image.Size;
                    size_of_image.Width += num_pixels_w- w % num_pixels_w;
                    w = (w + num_pixels_w-w % num_pixels_w) / num_pixels_w;
                    size_of_image.Height += num_pixels_h-h % num_pixels_h;
                    h = (h + num_pixels_h-h % num_pixels_h) / num_pixels_h;
                    im = new Bitmap((Bitmap)image,size_of_image);
                    for (int i =0;i<num_pixels_w; i++)
                    {
                        for (int j = 0; j < num_pixels_h; j++) {
                            //im.SetPixel(i,j,Color.Black);
                            int r = 0, g = 0, b = 0;
                            Color color;
                            for(int x = i * w; x < (i + 1) * w; x++)
                            {
                                for (int y = j * h; y < (j + 1) * h; y++)
                                {
                                    color = im.GetPixel(x, y);
                                    r += color.R;
                                    g += color.G;
                                    b += color.B;
                                }
                            }
                            r /= w * h;
                            g /= w * h;
                            b /= w * h;
                            for (int x = i * w; x < (i + 1) * w; x++)
                            {
                                for (int y = j * h; y < (j + 1) * h; y++)
                                {
                                    im.SetPixel(x, y, Color.FromArgb(r, g, b));
                                }
                            }
                        }
                    }
                    MessageBox.Show("work done");
                    
                }
                else
                {
                    MessageBox.Show("Wrong number of pixels");
                }
            }
            else
            {
                MessageBox.Show("Open file first");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(im != null)
            {
                Form2 form2 = new Form2(this);
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("No image");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this);
            form.ShowDialog();
        }
    }
}
