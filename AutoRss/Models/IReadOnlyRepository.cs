using System.Collections.Generic;

namespace AutoRss.Models
{
    public interface IReadOnlyRepository<out T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
