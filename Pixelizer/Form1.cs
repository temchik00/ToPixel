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
            int w = image.Width;
            int h = image.Height;
            using(Graphics g = Graphics.FromImage(image))
            {
                Pen p = new Pen(Color.Black, 1);
                //g.DrawLine(p, 0, 0, w, h);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "png|*.png";
            if(save.ShowDialog() == DialogResult.OK)
            {
                image.Save(@save.FileName);
            }
        }
    }
}
