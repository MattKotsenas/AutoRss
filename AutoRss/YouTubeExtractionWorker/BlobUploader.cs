using System;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AutoRss.YouTubeExtractionWorker
{
    public class BlobUploader
    {
        private readonly CloudBlobClient _client;
        private readonly string _containerName;

        public BlobUploader(CloudBlobClient client, string containerName)
        {
            // Retrieve storage account from connection string.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //CloudBlobClient blobClient = account.CreateCloudBlobClient();
            _client = client;
            _containerName = containerName;
        }

        public Uri Upload(string name, Stream stream)
        {
            var container = GetOrCreateContainer();
            var blobUrl = UploadToStorage(container, name, stream);

            return blobUrl;
        }

        private CloudBlobContainer GetOrCreateContainer()
        {
            var container = _client.GetContainerReference(_containerName);
            container.CreateIfNotExists();
            return container;
        }

        private Uri UploadToStorage(CloudBlobContainer container, string name, Stream stream)
        {
            var blob = container.GetBlockBlobReference(name);
            blob.UploadFromStream(stream);

            return blob.Uri;
        }
    }
}
