namespace AutoRss.Models
{
    public interface IWriteRepository<in T>
    {
        void Create(T item);
    }
}
