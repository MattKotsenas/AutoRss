using System;
using System.Collections.Generic;
using System.Linq;
using StrongerTypes.NonNullable;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.Models
{
    public class MediaItem : SyndicationItemBase
    {
        public int Id { get; private set; }

        public MediaItem(int id, NonNullable<string> name, NonNullable<Uri> detailsLink, NonNullable<Uri> downloadLink, DateTime created)
        {
            Id = id;
            Name = name;
            DetailsLink = detailsLink;
            DownloadLink = downloadLink;
            Created = created;
        }
    }
}