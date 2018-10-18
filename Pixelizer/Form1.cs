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
        public Form1()
        {
            InitializeComponent();
        }
        Image image;
        Bitmap im;
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
                    size_of_image.Width += w % num_pixels_w;
                    w = (w + w % num_pixels_w) / num_pixels_w;
                    size_of_image.Height += h % num_pixels_h;
                    h = (h + h % num_pixels_h) / num_pixels_h;
                    im = new Bitmap((Bitmap)image,size_of_image);
                   // im =new  Bitmap(im, size_of_image);
                    for (int i =0;i<w; i++)
                    {
                        for (int j = 0; j < h; j++) {
                            im.SetPixel(i,j,Color.Black);
                            
                        }
                    }
                    
                    
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
    }
}
