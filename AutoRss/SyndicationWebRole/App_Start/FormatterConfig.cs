using System.Web.Http;
using System.Web.Mvc;
using AutoRss.SyndicationFeedFormatter;

namespace AutoRss.SyndicationWebRole
{
    public class FormatterConfig
    {
        public static void RegisterFormatters(HttpConfiguration configuration)
        {
            var mapper = DependencyResolver.Current.GetService<ISyndicationFeedMapper>();

            configuration.Formatters.Add(new RssSyndicationFeedFormatter(mapper));
            configuration.Formatters.Add(new AtomSyndicationFeedFormatter(mapper));
        }
    }
}