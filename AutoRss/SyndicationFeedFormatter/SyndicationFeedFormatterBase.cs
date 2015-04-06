using System;
using System.IO;
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
        private readonly ISyndicationFeedMapper _mapper;

        protected SyndicationFeedFormatterBase(ISyndicationFeedMapper mapper, string supportedMediaType,
            string mappingName, string formattingQueryParam)
        {
            _mapper = mapper;
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(supportedMediaType));

            this.AddUriPathExtensionMapping(mappingName, new MediaTypeHeaderValue(supportedMediaType));
            this.AddQueryStringMapping(formattingQueryParam, mappingName, new MediaTypeHeaderValue(supportedMediaType));
        }

        public override bool CanReadType(Type type)
        {
            return _mapper.IsSupportedType(type);
        }

        public override bool CanWriteType(Type type)
        {
            return _mapper.IsSupportedType(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_mapper.IsSupportedType(type))
                {
                    using (var writer = XmlWriter.Create(writeStream))
                    {
                        var feed = _mapper.Map(value);
                        WriteFeed(feed, writer);
                    }
                }
            });
        }

        protected abstract void WriteFeed(SyndicationFeed feed, XmlWriter writer);
    }
}

