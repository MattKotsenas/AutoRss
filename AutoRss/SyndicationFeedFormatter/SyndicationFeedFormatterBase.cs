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
using System.Xml;

// Based on http://www.strathweb.com/2012/04/rss-atom-mediatypeformatter-for-asp-net-webapi/

namespace AutoRss.SyndicationFeedFormatter
{
    public abstract class SyndicationFeedFormatterBase : MediaTypeFormatter
    {
        protected readonly string Title;
        protected readonly string Description;

        protected SyndicationFeedFormatterBase(string title, string description, string supportedMediaType,
            string mappingName, string formattingQueryParam)
        {
            Title = title;
            Description = description;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(supportedMediaType));

            this.AddUriPathExtensionMapping(mappingName, new MediaTypeHeaderValue(supportedMediaType));
            this.AddQueryStringMapping(formattingQueryParam, mappingName, new MediaTypeHeaderValue(supportedMediaType));
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

            var feed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(Title),
                Description = new TextSyndicationContent(Description),
                Items = items.Select(BuildSyndicationItem)
            };

            using (var writer = XmlWriter.Create(stream))
            {
                WriteFeed(feed, writer);
            }
        }

        protected abstract void WriteFeed(SyndicationFeed feed, XmlWriter writer);

        private SyndicationItem BuildSyndicationItem(ISyndicationItem synItem)
        {
            var item = new SyndicationItem
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

