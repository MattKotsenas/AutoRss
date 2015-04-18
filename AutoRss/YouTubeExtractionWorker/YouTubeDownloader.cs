using System.IO;
using System.Net;

namespace AutoRss.YouTubeExtractionWorker
{
    public class YouTubeDownloader
    {
        public DownloadedItem Download(YouTubeItem item)
        {
            var request = WebRequest.Create(item.Url);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var mem = new MemoryStream();
                    stream.CopyTo(mem);

                    return new DownloadedItem
                    {
                        Name = item.Name,
                        OriginalUrl = item.Url,
                        ContentType = response.ContentType,
                        Size = response.ContentLength,
                        Stream = mem
                    };
                }
            }
        }
    }
}
