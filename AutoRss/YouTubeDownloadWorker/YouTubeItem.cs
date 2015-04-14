using System;
using System.IO;

namespace AutoRss.YouTubeDownloadWorker
{
    public class YouTubeItem
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
    }

    public class DownloadedItem
    {
        public string Name { get; set; }
        public Uri OriginalUrl { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public Stream Stream { get; set; }
        public Uri BlobUrl { get; set; }
    }
}
