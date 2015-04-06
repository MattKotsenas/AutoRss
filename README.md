# AutoRss

## Using the SyndicationFeedFormatter
1. Add this code to your ```Global.asax```

        ```csharp
        GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.RssSyndicationFeedFormatter("Feed Title", "Feed Description"));
        GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.AtomSyndicationFeedFormatter("Feed Title", "Feed Description"));
        ```

1. Call your API; you can specify RSS / Atom either via the Accept header, or by appending ```formatter=rss``` to your query string

## Running Unit Tests
1. Install the [MSpec Test Adapter](https://visualstudiogallery.msdn.microsoft.com/4abcb54b-53b5-4c44-877f-0397556c5c44) extension in Visual Studio
1. After restarting VS, tests should show up in the *Test Explorer* pane