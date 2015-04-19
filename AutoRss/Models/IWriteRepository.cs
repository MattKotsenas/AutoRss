using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRss.Models
{
    public interface IWriteRepository<in T>
    {
        void Create(T item);
    }
}
