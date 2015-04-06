using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.SyndicationFeedFormatter
{
    public interface ISyndicationFeedMapper
    {
        bool IsSupportedType(Type type);
        SyndicationFeed Map(object models);
    }
}
