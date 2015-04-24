using System;
using AutoRss.Models;

namespace AutoRss.WriterWorker
{
    internal class MediaWriter
    {
        private readonly IWriteRepository<MediaItem> _repo;

        public MediaWriter(IWriteRepository<MediaItem> repo)
        {
            _repo = repo;
        }

        public void Write(string url, string mimeType, long size, string name)
        {
            _repo.Create(new MediaItem
            {
                Created = DateTime.Now,
                DownloadLink = url,
                MimeType = mimeType,
                Name = name,
                Size = size
            });
        }
    }
}
