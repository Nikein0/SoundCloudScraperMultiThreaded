using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundCloudScraperV1._4
{
    public partial class Form1 : Form
    {
        private long totalbytes = 0;
        private long collectedbytes = 0;
        private static string songlink = "https://soundcloud.com/";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2ProgressBar1.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
