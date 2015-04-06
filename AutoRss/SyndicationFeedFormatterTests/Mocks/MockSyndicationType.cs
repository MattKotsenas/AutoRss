using System;
using StrongerTypes.NonNullable;

namespace AutoRss.SyndicationFeedFormatter.Tests.Mocks
{
    public class MockSyndicationType : ISyndicationItem
    {
        public NonNullable<string> Name
        {
            get { throw new NotImplementedException(); }
        }

        public NonNullable<Uri> DetailsLink
        {
            get { throw new NotImplementedException(); }
        }

        public NonNullable<Uri> DownloadLink
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime Created
        {
            get { throw new NotImplementedException(); }
        }
    }
}
