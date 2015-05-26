namespace AutoRss.Configuration
{
    public interface IConfiguration
    {
        bool UseMockMediaRepository { get; }
        string MicrosoftServiceBusConnectionString { get; }
        string MediaRepositoryConnectionString { get; }
        string CloudSaverStorageConnectionString { get; }
    }
}
