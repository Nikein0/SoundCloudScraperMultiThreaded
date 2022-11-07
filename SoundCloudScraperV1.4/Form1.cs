using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SoundCloudExplode;
using SoundCloudScraper;

namespace SoundCloudScraperV1._4
{
    public partial class Form1 : Form
    {
        public SoundCloudClient soundcloud = new SoundCloudClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2ProgressBar1.Visible = false;
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            SongInfo songinfo = new SongInfo();
            var stopwatch = new Stopwatch();
            var track = await soundcloud.Tracks.GetAsync(TxtURL.Text);
            guna2TextBox2.Text = $@"{songinfo.GetName(track.Title, track.User.Username)}   {songinfo.GetDuration(track.Duration)}   ";
            
            TimeSpan timespan = stopwatch.Elapsed;
            guna2TextBox2.Text += $@"{timespan.Minutes}:{timespan.Seconds}:{timespan.Milliseconds}";


        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void bgWorkerGetSong_DoWork(object sender, DoWorkEventArgs e)
        {
                
        }

        private async void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
