using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRss.Models.Mocks
{
    public class MockMediaRepository : IReadWriteRepository<MediaItem>, IReadOnlyRepositoryAsync<MediaItem>
    {
        private readonly IList<MediaItem> _items = new[]
        {
            new MediaItem(
                1,
                "Name1",
                "http://downloadLink1",
                DateTime.Now,
                0,
                "audio/mpeg"
            ),
            new MediaItem(
                2,
                "Name2",
                "http://downloadLink2",
                DateTime.Now,
                0,
                "audio/mp4"
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

        public void Create(MediaItem item)
        {
            _items.Add(item);
        }
    }
}
