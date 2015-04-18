using AutoRss.YouTubeExtractionWorker;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace AutoRss.YouTubeExtractionWorkerTests
{
    public abstract class YouTubeUrlExtractorTestsContext
    {
        Establish context = () =>
        {
            Subject = new YouTubeUrlExtractor();
        };

        protected static YouTubeUrlExtractor Subject;
        protected static YouTubeItem Item;
    }

    [Subject(typeof (YouTubeUrlExtractor))]
    public class When_the_YouTubeUrlExtractor_is_given_a_link_to_a_YouTube_page : YouTubeUrlExtractorTestsContext
    {
        Because of = () => Item = Subject.Download("https://www.youtube.com/watch?v=4w1e_JVhtxs");

        It should_return_the_download_item = () => Item.ShouldNotBeNull();
    }

    [Subject(typeof (YouTubeUrlExtractor))]
    public class When_the_YouTubeUrlExtractor_is_given_a_link_to_a_non_existent_page : YouTubeUrlExtractorTestsContext
    {
        Because of = () => Item = Subject.Download("http://example.org");

        It should_return_null = () => Item.ShouldBeNull();
    }
}
