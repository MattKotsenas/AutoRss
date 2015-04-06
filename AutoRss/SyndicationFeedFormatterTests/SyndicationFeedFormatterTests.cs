using Machine.Specifications;
using AutoRss.SyndicationFeedFormatter.Tests.Mocks;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace AutoRss.SyndicationFeedFormatter.Tests
{
    public abstract class SyndicationFeedFormatterContext
    {
        protected static SyndicationFeedFormatter Formatter;
        protected static bool CanWriteType;

        Establish context = () =>
        {
            Formatter = new SyndicationFeedFormatter("text/html" ,"title", "description");
        };
    }

    [Subject(typeof (SyndicationFeedFormatter))]
    public class When_a_type_implements_ISyndicationItem : SyndicationFeedFormatterContext
    {
        Because of = () => CanWriteType = Formatter.CanWriteType(typeof (MockSyndicationType));

        It should_be_writable_by_the_formatter = () => CanWriteType.ShouldBeTrue();
    }

    [Subject(typeof(SyndicationFeedFormatter))]
    public class When_a_type_does_not_implement_ISyndicationItem : SyndicationFeedFormatterContext
    {
        Because of = () => CanWriteType = Formatter.CanWriteType(typeof(object));

        It should_not_be_writable_by_the_formatter = () => CanWriteType.ShouldBeFalse();
    }
}
