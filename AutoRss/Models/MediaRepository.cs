using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRss.Models
{
    public class MediaRepository : DbContext, IReadWriteRepository<MediaItem>, IReadOnlyRepositoryAsync<MediaItem>
    {
        public MediaRepository(string connectionString) : base(connectionString)
        {
        }

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
        public void Create(MediaItem item)
        {
            MediaItems.Add(item);
            SaveChanges();
        }
    }
}
