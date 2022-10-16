using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SoundCloudExplode;
using SoundCloudScraper;

namespace SoundCloudScraperV1._4
{
    public partial class Form1 : Form
    {
        private long totalbytes = 0;
        private long collectedbytes = 0;
        private static string songlink = "https://soundcloud.com/";
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
            var track = await soundcloud.Tracks.GetAsync(TxtURL.Text);
            string trackname = track.Title;
            string creatorname = track.User.Username;
            string fullname = $@"{trackname} - {creatorname}";
            guna2TextBox2.Text = fullname;

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
    }
}
