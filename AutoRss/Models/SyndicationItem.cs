using System;
using System.Collections.Generic;
using System.Linq;
using StrongerTypes.NonNullable;
using System.Runtime.Serialization;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.Models
{
    [DataContract]
    public class SyndicationItem : ISyndicationItem
    {
        [DataMember]
        public NonNullable<string> Name { get; private set; }
        [DataMember]
        public NonNullable<Uri> DetailsLink { get; private set; }
        [DataMember]
        public NonNullable<Uri> DownloadLink { get; private set; }
        [DataMember]
        public DateTime Created { get; private set; }

        public SyndicationItem(NonNullable<string> name, NonNullable<Uri> detailsLink, NonNullable<Uri> downloadLink, DateTime created)
        {
            Name = name;
            DetailsLink = detailsLink;
            DownloadLink = downloadLink;
            Created = created;
        }
    }
}