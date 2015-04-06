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
            var repo = new MediaRepository();
            return repo.GetAll();
        }
    }
}
