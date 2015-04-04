using StrongerTypes.NonNullable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.SyndicationFeedFormatter
{
    [DataContract]
    public abstract class SyndicationItemBase : ISyndicationItem
    {
        [DataMember]
        public virtual NonNullable<string> Name { get; protected set; }
        [DataMember]
        public virtual NonNullable<Uri> DetailsLink { get; protected set; }
        [DataMember]
        public virtual NonNullable<Uri> DownloadLink { get; protected set; }
        [DataMember]
        public virtual DateTime Created { get; protected set; }
    }
}
