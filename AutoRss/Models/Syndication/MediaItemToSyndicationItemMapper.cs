using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.Models.Syndication
{
    public class MediaItemToSyndicationItemMapper : ISyndicationFeedMapper
    {
        private readonly string _title;
        private readonly string _description;

        public MediaItemToSyndicationItemMapper(string title, string description)
        {
            _title = title;
            _description = description;
        }

        private SyndicationItem MapItem(MediaItem item)
        {
            var synItem = new SyndicationItem
            {
                Title = new TextSyndicationContent(item.Name),
                PublishDate = item.Created,
                Content = new TextSyndicationContent(item.Name)
            };

            synItem.Links.Add(new SyndicationLink(item.DownloadLink));
            synItem.AddPermalink(item.DownloadLink);

            return synItem;
        }

        public bool IsSupportedType(Type type)
        {
            return (typeof(MediaItem).IsAssignableFrom(type) ||
                    typeof(IEnumerable<MediaItem>).IsAssignableFrom(type));
        }

        public SyndicationFeed Map(object models)
        {
            var items = new List<MediaItem>();
            if (models is IEnumerable<MediaItem>)
            {
                items.AddRange((IEnumerable<MediaItem>)models);
            }
            else
            {
                items.Add((MediaItem)models);
            }

            return new SyndicationFeed
            {
                Title = new TextSyndicationContent(_title),
                Description = new TextSyndicationContent(_description),
                Items = items.Select(MapItem)
            };
        }
    }
}
