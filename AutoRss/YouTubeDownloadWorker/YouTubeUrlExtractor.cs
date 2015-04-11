using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeExtractor;

namespace AutoRss.YouTubeDownloadWorker
{
    public class YouTubeUrlExtractor
    {
        public string Download(string youtubeLink)
        {
            IEnumerable<VideoInfo> videoInfos;
            try
            {
                videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeLink);
            }
            catch (ArgumentException)
            {
                return null;
            }

            var video = videoInfos
                    .Where(info => info.VideoType == VideoType.Mp4)
                    .OrderByDescending(info => info.Resolution).FirstOrDefault();

            if (video == null) return null;

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }
            return video.DownloadUrl;
        }
    }
}
