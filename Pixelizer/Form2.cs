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
    public partial class Form2 : Form
    {
        Form1 form1;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = form1.im;
            this.Size = pictureBox1.Size;
        }
    }
}
