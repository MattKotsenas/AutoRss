using System.ServiceModel.Syndication;
using System.Xml;

namespace AutoRss.SyndicationFeedFormatter
{
    public class AtomSyndicationFeedFormatter : SyndicationFeedFormatterBase
    {
        private const string MediaType = "application/atom+xml";

        public AtomSyndicationFeedFormatter(string title, string description, string formattingQueryParam = "formatter")
            : base(title, description, MediaType, "atom", formattingQueryParam)
        {
        }

        protected override void WriteFeed(SyndicationFeed feed, XmlWriter writer)
        {
            var formatter = new Atom10FeedFormatter(feed);
            formatter.WriteTo(writer);
        }
    }
}