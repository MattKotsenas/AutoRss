using System.Collections.Generic;
using System.Web.Http;
using AutoRss.Models;

namespace AutoRss.SyndicationWebRole.Controllers
{
    public class FeedController : ApiController
    {
        public IEnumerable<MediaItem> Get()
        {
            var repo = new MediaRepository();
            return repo.GetAll();
        }
    }
}
