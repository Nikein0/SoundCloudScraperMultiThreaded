using System;
using System.IO;
using System.Diagnostics;
using SoundCloudExplode;
using Guna.UI2.WinForms;
using System.Threading.Tasks;
using SoundCloudExplode.Track;

namespace SoundCloudScraper
{
    class SoundCloud
    {
        public SoundCloudClient soundcloud = new SoundCloudClient();
    }
    class Downloader : SoundCloud
    {
        public async void Download(TrackInformation track)   //@$"c:\Downloads\{trackname}.mp3"
        {
            await soundcloud.DownloadAsync(track, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{track.User.Username} - {track.Title}.mp3");
        }
    }
    class SongInfo : SoundCloud
    {
        public string GetName(string songname, string username)
        {
            string fullname = $@"{username} - {songname}";
            return fullname;
        }

        

        public string GetDuration(long? length)
        {
            long? seconds = length /1000;
            long? minutes = seconds /60;
            long? hours = minutes /60;
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
}
        
    
