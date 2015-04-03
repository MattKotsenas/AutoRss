using StrongerTypes.NonNullable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.SyndicationFeedFormatter
{
    public interface ISyndicationItem
    {
        NonNullable<string> Name { get; }
        NonNullable<Uri> DetailsLink { get; }
        NonNullable<Uri> DownloadLink { get; }
        DateTime Created { get; }
    }
}
