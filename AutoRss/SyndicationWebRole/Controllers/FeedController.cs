using System.Collections.Generic;
using System.Web.Http;
using AutoRss.Models;

namespace AutoRss.SyndicationWebRole.Controllers
{
    public class FeedController : ApiController
    {
        private readonly IReadOnlyRepository<MediaItem> _repository;

        public FeedController(IReadOnlyRepository<MediaItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<MediaItem> Get()
        {
            return _repository.GetAll();
        }
    }
}
