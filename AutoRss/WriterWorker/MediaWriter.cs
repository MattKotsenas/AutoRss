using System;
using AutoRss.Models;
using Microsoft.ServiceBus.Messaging;

namespace AutoRss.WriterWorker
{
    internal class MediaWriter
    {
        private readonly IWriteRepository<MediaItem> _repo;

        public MediaWriter(IWriteRepository<MediaItem> repo)
        {
            _repo = repo;
        }

        public void Write(BrokeredMessage message)
        {
            _repo.Create(new MediaItem
            {
                Created = DateTime.Now,
                DownloadLink = message.Properties["Url"].ToString(),
                MimeType = "video/mp4",
                Name = message.Properties["Name"].ToString(),
                Size = 0
            });
        }
    }
}
