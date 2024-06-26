﻿using System;
using System.IO;
using System.Diagnostics;
using SoundCloudExplode;
using Guna.UI2.WinForms;
using System.Threading.Tasks;
using SoundCloudExplode.Track;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace SoundCloudScraper
{

    public class SoundCloud
    {
        public SoundCloudClient soundcloud = new SoundCloudClient();
        public virtual string getSpecName() { return null; } //dinaminis polimorfizmas
    }
    public class Downloader : SoundCloud
    {
        public bool isDownloadable = true;
        public TimeSpan timespan;
        private string specname = "";
        private int downloadcount = 0;
        private string filePath = "bulk.txt";
        private List<string> links;
        
        public Downloader(List<string> links)
        {
            this.links = links;
        }
        public Downloader()
        {

        }
        public List<string> getLinks()
        {
            return links;
        }
        //string trackname;
        public void SetTimespan(TimeSpan timespan)
        {
            this.timespan = timespan;
        }
        public void SetSpecName(string specname) { this.specname = specname; }

        public void UpdateDwnlCount() { this.downloadcount = downloadcount + 1; }

        public int GetDownlCount()
        {
            return this.downloadcount;
        }

        /*public async Task Download(string SClink)   //@$"c:\Downloads\{trackname}.mp3"
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var track = await soundcloud.Tracks.GetAsync(SClink);
            UpdateDwnlCount();
            SetSpecName(track.Title);
            await soundcloud.DownloadAsync(track, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{track.User.Username} - {track.Title}.mp3");
            stopwatch.Stop();
            
            SetTimespan(stopwatch.Elapsed);
        }*/
        public async Task Download(string SClink)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var track = await soundcloud.Tracks.GetAsync(SClink);

            UpdateDwnlCount();
            SetSpecName(track.Title);

            await Task.Run(() => soundcloud.DownloadAsync(track, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{track.User.Username} - {track.Title}.mp3"));

            stopwatch.Stop();
            SetTimespan(stopwatch.Elapsed);

        }
        
        public List<string> ReadLinks()
        {
            
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    // Read the file line by line and extract links.
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Check if the line contains a valid URL.
                        if (IsValidUrl(line))
                        {
                            links.Add(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

            return links;
        }
        private bool IsValidUrl(string url)
        {
            // You can implement a more sophisticated URL validation here if needed.
            // This is a simple check to see if the line starts with "http://" or "https://".
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
        public TimeSpan GetTimespan()
        {
            return timespan;
        }
        public override string getSpecName()
        {
            string specialname = $@"{downloadcount} {specname}";
            return specialname;
        }
        

    }

    class Song : SoundCloud
    {
        


        private string SongName;
        private string UserName;
        private long? Duration;

        public Song(string songName, string userName, long? duration) {
            this.UserName = userName;
            this.SongName = songName;
            this.Duration = duration;
        }
        
        public string getSongName() { return SongName; }
        public string getUserName() { return UserName; }
        public long? getDuration() { return Duration; }
        public string getDurationString() { return DurationString; }

        public string DurationString
        {
            get
            {
                long? seconds = Duration / 1000;
                long? minutes = seconds / 60;
                long? hours = minutes / 60;
                seconds = seconds % 60;
                minutes = minutes % 60;
                if (hours <= 0)
                {
                    string full = $@"{minutes}:{seconds}";
                    return full;
                }
                else
                {
                    string full = $@"{hours}:{minutes}:{seconds}";
                    return full;
                }
            }
        }
        public override string getSpecName()
        { 
                string fullname = $@"{SongName} - {UserName}";
                return fullname;
        }
        }
        
        

    }
    public class SongSerialization
    {
        private string name;
        private string username;
        private long? duration;

        public void setname(string name1)
        {
            name = name1;
        }
        public void setusername(string name1)
        {
            username = name1;
        }
        public void setduration(long? dur1)
        {
            duration = dur1;
        }
        public string getname() { return name; }
        public string getusername() { return username; }
        public long? getduration() { return duration; }
    }

interface SongStacking   //Sasaja
{
    void addLink(int place, string link);
    string getLink(int place);
    void addSongCount();
    int getSongCount();


}
class SongStack : SongStacking  //Sasaja implement
{
    private int songcount = 0;
    private string[] links = new string[10];
    public void addLink(int place, string link) { links[place] = link; }
    public string getLink(int place) { return links[place]; }
    public void addSongCount()
    {
        songcount++;
    }
    public int getSongCount() { return songcount; }
}

