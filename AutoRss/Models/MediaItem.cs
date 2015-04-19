using System;

namespace AutoRss.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DownloadLink { get; set; }
        public DateTime Created { get; set; }
        public long Size { get; set; }
        public string MimeType { get; set; }


        public MediaItem(int id, string name, string downloadLink, DateTime created, long size, string mimeType)
        {
            Id = id;
            Name = name;
            DownloadLink = downloadLink;
            Created = created;
            Size = size;
            MimeType = mimeType;
        }

        public MediaItem()
        {
        }
    }
}