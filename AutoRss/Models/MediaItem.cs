using System;

namespace AutoRss.Models
{
    public class MediaItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Uri DownloadLink { get; private set; }
        public DateTime Created { get; private set; }


        public MediaItem(int id, string name, Uri downloadLink, DateTime created)
        {
            Id = id;
            Name = name;
            DownloadLink = downloadLink;
            Created = created;
        }
    }
}