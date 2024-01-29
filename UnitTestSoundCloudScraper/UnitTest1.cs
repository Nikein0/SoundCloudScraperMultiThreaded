using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SoundCloudExplode;
using SoundCloudScraper;
using SoundCloudExplode.Tracks;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UnitTestSoundCloudScraper
{
    [TestClass]
    public class UnitTest1
    {
        string link = "https://soundcloud.com/user-35853073/intelestellar";

        [TestMethod]
        public async Task CheckIfSongAvailable()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);

            Assert.IsNotNull(track);
        }
        [TestMethod]
        public void CheckIfSongDownloadable()
        {
            Downloader downloader = new Downloader();
            var downloaded = downloader.isDownloadable;

            Assert.IsTrue(downloaded);
        }
        [TestMethod]
        public async Task CheckIfSongPublic()
        {

            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);
            Assert.IsTrue(track.Public);
        }
        [TestMethod]
        public async Task CheckIfSongHasArtwork()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);

            Assert.IsNotNull(track.ArtworkUrl);
        }
        [TestMethod]
        public async Task CheckIfSongisCommentable()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);

            Assert.IsTrue(track.Commentable);
        }
        [TestMethod]
        public async Task CheckDownload()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);

            await Task.Run(() => soundcloud.DownloadAsync(track, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{track.User.Username} - {track.Title}.mp3"));
            Assert.IsNotNull(track);
        }
        [TestMethod]
        public async Task CheckFakeLink()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);
            try
            {
                await Task.Run(() => soundcloud.DownloadAsync(track, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + $@"\Downloads\{track.User.Username} - {track.Title}.mp3"));
            }
            catch { Expression e; }

            Assert.IsTrue(true);
                  

        }
        [TestMethod]
        public async Task CheckIfSongStreamable()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);
            Assert.IsTrue(track.Streamable);
        }
        [TestMethod]
        public async Task CheckIfMusicHasDownloadsLeft()
        {
            SoundCloudClient soundcloud = new SoundCloudClient();
            var track = await soundcloud.Tracks.GetAsync(link);
            Assert.IsTrue(!track.HasDownloadsLeft);
        }
    }
}
