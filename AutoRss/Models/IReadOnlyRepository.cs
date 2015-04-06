using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.Models
{
    public interface IReadOnlyRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
