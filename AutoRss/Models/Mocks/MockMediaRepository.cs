using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRss.Models.Mocks
{
    public class MockMediaRepository : IReadOnlyRepository<MediaItem>, IReadOnlyRepositoryAsync<MediaItem>
    {
        private readonly IEnumerable<MediaItem> _items = new[]
        {
            new MediaItem(
                1,
                "Name1",
                new Uri("http://downloadLink1"),
                DateTime.Now
                ),
            new MediaItem(
                2,
                "Name2",
                new Uri("http://downloadLink2"),
                DateTime.Now
                )
        };

        public IEnumerable<MediaItem> GetAll()
        {
            return _items;
        }

        public MediaItem Get(int id)
        {
            return _items.SingleOrDefault(item => item.Id == id);
        }

        public async Task<IEnumerable<MediaItem>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        public async Task<MediaItem> GetAsync(int id)
        {
            return await Task.FromResult(Get(id));
        }
    }
}
