# AutoRss

## Using the SyndicationFeedFormatter
1. Create an instance of ```ISyndicationFeedMapper``` to map from your domain types to ```SyndicationItem```
1. Add this code to your ```Global.asax```

        ```csharp
        var mapper = new MyDomainObjectToSyndicationItemMapper();
        GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.RssSyndicationFeedFormatter(mapper));
        GlobalConfiguration.Configuration.Formatters.Add(new SyndicationFeedFormatter.AtomSyndicationFeedFormatter(mapper));
        ```

1. Call your API; you can specify RSS / Atom either via the Accept header, or by appending ```formatter=rss``` to your query string

## Running Unit Tests
1. Install the [MSpec Test Adapter](https://visualstudiogallery.msdn.microsoft.com/4abcb54b-53b5-4c44-877f-0397556c5c44) extension in Visual Studio
1. After restarting VS, tests should show up in the *Test Explorer* pane