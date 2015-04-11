using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.YouTubeDownloadWorker
{
    public class YouTubeItem
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public long Size { get; set; }
    }
}
