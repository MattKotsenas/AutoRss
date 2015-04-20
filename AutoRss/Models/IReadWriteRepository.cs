namespace AutoRss.Models
{
    public interface IReadWriteRepository<T> : IReadOnlyRepository<T>, IWriteRepository<T>
    {
    }
}
