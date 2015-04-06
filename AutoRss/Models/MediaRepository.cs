using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using StrongerTypes.NonNullable;

namespace AutoRss.Models
{
    public class MediaRepository : DbContext, IReadOnlyRepository<MediaItem>, IReadOnlyRepositoryAsync<MediaItem>
    {
        public IEnumerable<MediaItem> GetAll()
        {
            return new[] {
                new MediaItem(
                    1,
                    new NonNullable<string>("Name1"),
                    new NonNullable<Uri>(new Uri("http://detailsLink1")),
                    new NonNullable<Uri>(new Uri("http://downloadLink1")),
                    DateTime.Now
                ),
                new MediaItem(
                    2,
                    new NonNullable<string>("Name2"),
                    new NonNullable<Uri>(new Uri("http://detailsLink2")),
                    new NonNullable<Uri>(new Uri("http://downloadLink2")),
                    DateTime.Now
                )
            };
            //return MediaItems;
        }
        public MediaItem Get(int id)
        {
            return MediaItems.SingleOrDefault(item => item.Id == id);
        }

        public async Task<IEnumerable<MediaItem>> GetAllAsync()
        {
            return await MediaItems.ToListAsync();
        }
        public async Task<MediaItem> GetAsync(int id)
        {
            return await MediaItems.SingleOrDefaultAsync(item => item.Id == id);
        }

        public DbSet<MediaItem> MediaItems { get; set; }
    }
}
