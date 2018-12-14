using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace Pixelizer
{

    //@"..\..\input.txt"

   
    public partial class Form1 : Form
    {
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - dataGridView1.Top - 40;
            
        }
        public Form1()
        {
            InitializeComponent();
        }
        Image image;
        public Bitmap im;
        Color[,,] colors = new Color[256, 256, 256];
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
                    if (size_of_image.Width % num_pixels_w != 0)
                    {
                        size_of_image.Width += num_pixels_w - size_of_image.Width % num_pixels_w;
                    }
                    w = size_of_image.Width / num_pixels_w;
                    if (size_of_image.Width % num_pixels_w != 0)
                    {
                        size_of_image.Height += num_pixels_h - size_of_image.Height % num_pixels_h;
                    }
                    h = size_of_image.Height/ num_pixels_h;
                    im = new Bitmap((Bitmap)image,size_of_image);
                    if (checkBox2.Checked)
                    {
                        int simpl = 4;
                        for (int i = 0; i < im.Width; i++)
                        {
                            for (int j = 0; j < im.Height; j++)
                            {
                                Color color = im.GetPixel(i, j);
                                int r = color.R / simpl * simpl, g = color.G / simpl * simpl, b = color.B / simpl * simpl;
                                im.SetPixel(i, j, Color.FromArgb(r, g, b));
                            }

                        }

                    }
                    progressBar1.Maximum = num_pixels_w;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    for (int i =0;i<num_pixels_w; i++)
                    {
                        for (int j = 0; j < num_pixels_h; j++)
                        {
                            int r = 0, g = 0, b = 0;

                            Color color;
                            for (int x = i * w; x < (i + 1) * w; x++)
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
                            if (checkBox1.Checked)
                            {
                                if (dataGridView1.Rows.Count == 0)
                                {
                                    MessageBox.Show("no colors");
                                }
                                else
                                {
                                    if (colors[r, g, b].A != 0)
                                    {
                                        r = colors[r, g, b].R;
                                        g = colors[r, g, b].G;
                                        b = colors[r, g, b].B;
                                    }
                                    else
                                    {
                                        int min = 0;
                                        int min_r, min_g, min_b, c_r, c_g, c_b;
                                        for (int color_num = 1; color_num < dataGridView1.Rows.Count; color_num++)
                                        {
                                            min_r = Convert.ToInt32(dataGridView1[0, min].Value);
                                            min_g = Convert.ToInt32(dataGridView1[1, min].Value);
                                            min_b = Convert.ToInt32(dataGridView1[2, min].Value);
                                            c_r = Convert.ToInt32(dataGridView1[0, color_num].Value);
                                            c_g = Convert.ToInt32(dataGridView1[1, color_num].Value);
                                            c_b = Convert.ToInt32(dataGridView1[2, color_num].Value);
                                            //30 * (Ri - R0)2 + 59 * (Gi - G0)2 + 11 * (Bi - B0)2
                                            if ((30 * (c_r - r) * (c_r - r) + 59 * (c_g - g) * (c_g - g) + 11 * (c_b - b) * (c_b - b)) <=
                                                (30 * (min_r - r) * (min_r - r) + 59 * (min_g - g) * (min_g - g) + 11 * (min_b - b) * (min_b - b)))
                                            {
                                                min = color_num;
                                            }
                                        }
                                        r = Convert.ToInt32(dataGridView1[0, min].Value);
                                        g = Convert.ToInt32(dataGridView1[1, min].Value);
                                        b = Convert.ToInt32(dataGridView1[2, min].Value);
                                        colors[r, g, b] = Color.FromArgb(255, r, g, b);
                                    }
                                }
                            }
                            for (int x = i * w; x < (i + 1) * w; x++)
                            {
                                for (int y = j * h; y < (j + 1) * h; y++)
                                {
                                    im.SetPixel(x, y, Color.FromArgb(r, g, b));
                                }
                            }
                        }
                        progressBar1.Value = i+1;
                    }
                    progressBar1.Visible = false;
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
        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.Height - dataGridView1.Top-40;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "pixel palette|*.ppltte";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string[] text = new string[dataGridView1.Rows.Count];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    text[i] = dataGridView1[0, i].Value.ToString() + " " + dataGridView1[1, i].Value.ToString() + " " + dataGridView1[2, i].Value.ToString();
                }
                File.WriteAllLines(@save.FileName, text, Encoding.UTF8);
                
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "pixel palette|*.ppltte";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(@dialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                string s = reader.ReadToEnd();
                string[] rgb = new string[10];
                rgb = s.Trim().Split('\n');
                dataGridView1.Rows.Clear();
                for(int i = 0; i < rgb.Length; i++)
                {
                    string[] colors = rgb[i].Trim().Split();
                    dataGridView1.Rows.Add(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
                }
            }
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 256; y++)
                {
                    for (int z = 0; z < 256; z++)
                    {
                        colors[x, y, z] = Color.FromArgb(0, 0, 0, 0);
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(image != null)
            {
                myBitmap image_hsv =new myBitmap();
                image_hsv.set_bitmap((Bitmap)image);
                image_hsv.clear();
                im = image_hsv.bitmap;
            }
        }
    }
}
public class hsv
{
    public double Hue, Saturation, Value;

    public hsv()
    {
        this.Hue = 0;
        this.Saturation = 0;
        this.Value = 0;
    }
    
    public hsv(Color origin)
    {
        ColorToHSV(origin, out Hue, out Saturation, out Value);
    }
    public void setColor(Color origin)
    {
        ColorToHSV(origin, out Hue, out Saturation, out Value);
    }
    public Color GetColor()
    {
        return ColorFromHSV(this.Hue, this.Saturation, this.Value);
    }


    public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
    {
        int max = Math.Max(color.R, Math.Max(color.G, color.B));
        int min = Math.Min(color.R, Math.Min(color.G, color.B));

        hue = color.GetHue();
        saturation = (max == 0) ? 0 : 1d - (1d * min / max);
        value = max / 255d;
    }

    public static Color ColorFromHSV(double hue, double saturation, double value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        double f = hue / 60 - Math.Floor(hue / 60);

        value = value * 255;
        int v = Convert.ToInt32(value);
        int p = Convert.ToInt32(value * (1 - saturation));
        int q = Convert.ToInt32(value * (1 - f * saturation));
        int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        if (hi == 0)
            return Color.FromArgb(255, v, t, p);
        else if (hi == 1)
            return Color.FromArgb(255, q, v, p);
        else if (hi == 2)
            return Color.FromArgb(255, p, v, t);
        else if (hi == 3)
            return Color.FromArgb(255, p, q, v);
        else if (hi == 4)
            return Color.FromArgb(255, t, p, v);
        else
            return Color.FromArgb(255, v, p, q);
    }
    public Color GetColorRGB()
    {
        return hsv.ColorFromHSV(this.Hue, this.Saturation, this.Value);
    }
    public override string ToString()
    {
        return this.Hue.ToString() + " " + this.Saturation.ToString() + " " + this.Value.ToString();
    }

}
public class myBitmap
{
    public Bitmap bitmap;
    hsv[,] Colors;
    public myBitmap()
    {
        bitmap = new Bitmap(1, 1);
        Colors = new hsv[1,1];
        Colors[0, 0] = new hsv();
    }
    public myBitmap(Bitmap image)
    {
        bitmap = image;
        Colors = new hsv[image.Width,image.Height];
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                Colors[i, j] = new hsv();
                Colors[i,j].setColor(bitmap.GetPixel(i, j));
            }
        }
    }
    public void update()
    {
        for (int i = 0; i < bitmap.Width; i++)
        {
            for (int j = 0; j < bitmap.Height; j++)
            {
                bitmap.SetPixel(i, j, Colors[i,j].GetColorRGB());
            }
        }
    }
    public void setPixel(int x, int y, Color color)
    {
        bitmap.SetPixel(x, y, color);
        Colors[x,y].setColor(color);
    }
    public void setPixel(int x, int y, hsv color)
    {
        bitmap.SetPixel(x, y, color.GetColorRGB());
        Colors[x,y] = color;
    }
    public void set_bitmap(Bitmap image)
    {
        bitmap = new Bitmap(image, image.Size);
        Colors = new hsv[image.Width, image.Height];
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                Colors[i, j] = new hsv();
                Colors[i,j].setColor(bitmap.GetPixel(i, j));
            }
        }
    }
    public Color getPixelRGB(int x, int y)
    {
        return bitmap.GetPixel(x, y);
    }
    public hsv getPixelHSV(int x, int y)
    {
        return Colors[x,y];
    }
    public void clear()
    {
        for(int i =0;i< bitmap.Width; i++)
        {
            for(int j = 0; j< bitmap.Height; j++)
            {
                Colors[i,j].Saturation = 0.9;
                //Colors[i,j].Value = 0.8;
                this.setPixel(i, j, Colors[i,j]);
            }
        }
        
    }
}