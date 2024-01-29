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
using Guna.UI2.WinForms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Runtime.Remoting.Contexts;
using MySqlX.XDevAPI.Relational;

namespace SoundCloudScraperV1._4
{
    
    public partial class Form1 : Form
    {
        
        private SoundCloudClient soundcloud = new SoundCloudClient();
        private string linktext = "https://soundcloud.com/";
        SongStack songStack = new SongStack();
        private MySqlConnection GetCon()
        {
            string constr = "SERVER=localhost;DATABASE=soundcloudscraper;UID=root;PASSWORD=;";
            MySqlConnection con = new MySqlConnection(constr);
            con.Open();

            return con;
        }
        public Form1()
        {
            InitializeComponent();
            
            guna2ProgressBar1.Visible = true;
            guna2TextBox2.Visible = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            //var stopwatch = new Stopwatch();

            if (TxtURL.Text.Contains(linktext) == true)
            {

                var track = await soundcloud.Tracks.GetAsync(TxtURL.Text);
                Song song = new Song(track.Title, track.User.Username, track.Duration);    
                pictureBox1.Load(track.ArtworkUrl.ToString());
                Downloader downloader = new Downloader();

                guna2TextBox2.Visible = true;
                guna2TextBox2.Text = $@"{song.getSpecName()}";
                await downloader.Download(TxtURL.Text);
                guna2ProgressBar1.Increment(100);
                TimeSpan timespan = downloader.GetTimespan();
                string addDownloader = "insert into downloader(link) values('" + TxtURL.Text + "');";
                TxtURL.Text = "";
                guna2TextBox2.Text += " ";
                guna2TextBox2.Text += $@"{timespan.Minutes}:{timespan.Seconds}:{timespan.TotalMilliseconds}";
                guna2TextBox2.Text += "\r\n Download complete";
                MySqlCommand cmd = new MySqlCommand(addDownloader, GetCon());
                
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                //MessageBox.Show(i.ToString());
                /*
                SongSerialization songXML = 
                songXML.setname(track.Title);
                songXML.setusername(track.User.Username);
                songXML.setduration(track.Duration);
                XmlSerializer serializer = new XmlSerializer(typeof(SongSerialization));
                var sww = new StringWriter();
                XmlWriter writer = XmlWriter.Create(sww);
                serializer.Serialize(writer, songXML);
                String xml = sww.ToString();
                TextReader reader = new StringReader(xml);
                var myObject = (SongSerialization)serializer.Deserialize(reader);*/

                string addSong = "insert into songs(fullname, duration, downl_id) values('" + song.getSpecName() + "', '" + song.getDurationString() + "', '" + id + "');";
                MySqlCommand cmdd = new MySqlCommand(addSong, GetCon());
                cmdd.ExecuteNonQuery();




            }
            
            else
            {
                guna2TextBox2.Text = "Invalid link";
            }


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

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            //SongStacking songStack = new SongStacking();
            
            songStack.addLink(songStack.getSongCount(), TxtURL.Text);
            

            TxtURL.Text = "";
            guna2TextBox2.Text = $@"Added: {songStack.getLink(songStack.getSongCount())}";
            songStack.addSongCount();
        }

        private void DownloadAll_Button_Click(object sender, EventArgs e)
        {
            guna2TextBox2.Text = " ";
            Task[] downloadTasks = new Task[songStack.getSongCount()];

            var stopwatch = new Stopwatch();
            stopwatch.Start();



            //for (int i = 0; i < songStack.getSongCount(); i++)
            Parallel.For(0, songStack.getSongCount(), async i =>
                {
                    //guna2TextBox2.Text = "";
                    var track = await soundcloud.Tracks.GetAsync(songStack.getLink(i));
                    Song song = new Song(track.Title, track.User.Username, track.Duration);
                    //pictureBox1.Load(track.ArtworkUrl.ToString());


                    Downloader downloader = new Downloader();

                    //guna2TextBox2.Visible = true;
                    //guna2TextBox2.Text = $@"{song.getSpecName()}";
                    await downloader.Download(songStack.getLink(i));
                    guna2ProgressBar1.Increment(100);
                    TimeSpan timespan = downloader.GetTimespan();
                    //guna2TextBox2.Text += " ";
                    //guna2TextBox2.Text += $@"{timespan.Minutes}:{timespan.Seconds}:{timespan.TotalMilliseconds}";
                    //guna2TextBox2.Text += "\r\n Download complete";
                    //guna2ProgressBar1.Decrement(100);

                });
            stopwatch.Stop();
            guna2TextBox2.Text += " ";
            guna2TextBox2.Text += $@"{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}:{stopwatch.Elapsed.TotalMilliseconds}";
            guna2TextBox2.Text += $@" Downloaded {songStack.getSongCount()} songs";
            guna2ProgressBar1.Decrement(100);


        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}