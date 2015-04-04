using System;
using System.Collections.Generic;
using System.Linq;
using StrongerTypes.NonNullable;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.Models
{
    public class MediaItem : SyndicationItemBase
    {
        public MediaItem(NonNullable<string> name, NonNullable<Uri> detailsLink, NonNullable<Uri> downloadLink, DateTime created)
        {
            Name = name;
            DetailsLink = detailsLink;
            DownloadLink = downloadLink;
            Created = created;
        }
    }
}