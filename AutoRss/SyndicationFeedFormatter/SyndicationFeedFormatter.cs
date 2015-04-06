using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

// Based on http://www.strathweb.com/2012/04/rss-atom-mediatypeformatter-for-asp-net-webapi/

namespace AutoRss.SyndicationFeedFormatter
{
    public class SyndicationFeedFormatter : MediaTypeFormatter
    {
        private readonly string _rss = "application/rss+xml";
        private readonly string _title;
        private readonly string _description;

        public SyndicationFeedFormatter(string format, string title, string description)
        {
            _title = title;
            _description = description;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(_rss));

            this.AddUriPathExtensionMapping("rss", new MediaTypeHeaderValue(format));
            this.AddQueryStringMapping("formatter", "rss", new MediaTypeHeaderValue(format));
        }

        private bool IsSupportedType(Type type)
        {
            return (typeof (ISyndicationItem).IsAssignableFrom(type) ||
                    typeof (IEnumerable<ISyndicationItem>).IsAssignableFrom(type));
        }

        public override bool CanReadType(Type type)
        {
            return IsSupportedType(type);
        }

        public override bool CanWriteType(Type type)
        {
            return IsSupportedType(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsSupportedType(type))
                {
                    BuildSyndicationFeed(value, writeStream);
                }
            });
        }

        private void BuildSyndicationFeed(object models, Stream stream)
        {
            var items = new List<ISyndicationItem>();
            if (models is IEnumerable<ISyndicationItem>)
            {
                items.AddRange((IEnumerable<ISyndicationItem>)models);
            }
            else
            {
                items.Add((ISyndicationItem)models);
            }

            var feed = new SyndicationFeed()
            {
                Title = new TextSyndicationContent(_title),
                Description = new TextSyndicationContent(_description),
                Items = items.Select(item => BuildSyndicationItem(item))
            };

            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed, false);
                rssFormatter.WriteTo(writer);
            }
        }

        private SyndicationItem BuildSyndicationItem(ISyndicationItem synItem)
        {
            var item = new SyndicationItem()
            {
                Title = new TextSyndicationContent(synItem.Name),
                PublishDate = synItem.Created,
                Content = new TextSyndicationContent(synItem.Name)
            };

            item.Links.Add(new SyndicationLink(synItem.DetailsLink));
            item.AddPermalink(synItem.DownloadLink);

            return item;
        }
    }
}

