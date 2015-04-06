using System.ServiceModel.Syndication;
using System.Xml;

namespace AutoRss.SyndicationFeedFormatter
{
    public class RssSyndicationFeedFormatter : SyndicationFeedFormatterBase
    {
        private const string MediaType = "application/rss+xml";

        public RssSyndicationFeedFormatter(ISyndicationFeedMapper mapper, string formattingQueryParam = "formatter")
            : base(mapper, MediaType, "rss", formattingQueryParam)
        {
        }

        protected override void WriteFeed(SyndicationFeed feed, XmlWriter writer)
        {
            var formatter = new Rss20FeedFormatter(feed, false);
            formatter.WriteTo(writer);
        }
    }
}