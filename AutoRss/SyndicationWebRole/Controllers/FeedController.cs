using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoRss.SyndicationFeedFormatter;
using AutoRss.Models;
using StrongerTypes.NonNullable;

namespace AutoRss.SyndicationWebRole.Controllers
{
    public class FeedController : ApiController
    {
        public IEnumerable<ISyndicationItem> Get()
        {
            return new[] {
                new MediaItem(
                    new NonNullable<string>("Name1"),
                    new NonNullable<Uri>(new Uri("http://detailsLink1")),
                    new NonNullable<Uri>(new Uri("http://downloadLink1")),
                    DateTime.Now
                ),
                new MediaItem(
                    new NonNullable<string>("Name2"),
                    new NonNullable<Uri>(new Uri("http://detailsLink2")),
                    new NonNullable<Uri>(new Uri("http://downloadLink2")),
                    DateTime.Now
                )
            };
        }
    }
}
