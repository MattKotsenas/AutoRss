namespace AutoRss.Models
{
    public interface IConfiguration
    {
        bool UseMockMediaRepository { get; }
        string MicrosoftServiceBusConnectionString { get; }
        string MediaRepositoryConnectionString { get; }
    }
}
