using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AutoRss.Models
{
    public class MediaRepository : DbContext, IReadOnlyRepository<MediaItem>, IReadOnlyRepositoryAsync<MediaItem>
    {
        public IEnumerable<MediaItem> GetAll()
        {
            return MediaItems;
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
