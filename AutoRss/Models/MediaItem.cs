using System;

namespace AutoRss.Models
{
    public class MediaItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string DownloadLink { get; private set; }
        public DateTime Created { get; private set; }
        public long Size { get; private set; }
        public string MimeType { get; private set; }


        public MediaItem(int id, string name, string downloadLink, DateTime created, long size, string mimeType)
        {
            Id = id;
            Name = name;
            DownloadLink = downloadLink;
            Created = created;
            Size = size;
            MimeType = mimeType;
        }

        // EF's constructor
        // ReSharper disable once UnusedMember.Local
        private MediaItem()
        {
        }
    }
}