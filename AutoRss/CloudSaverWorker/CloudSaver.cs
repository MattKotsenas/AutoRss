using System;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AutoRss.CloudSaverWorker
{
    public class CloudSaver
    {
        private readonly CloudBlobContainer _container;

        public CloudSaver(CloudBlobContainer container)
        {
            _container = container;
        }

        public Tuple<Uri, string, long> Save(Uri url, string name)
        {
            var request = WebRequest.CreateHttp(url);
            var response = request.GetResponse();

            var length = response.ContentLength;
            var mediaType = response.ContentType;

            using (var stream = response.GetResponseStream())
            {
                name = name.ToShaHash();
                var blob = _container.GetBlockBlobReference(name);
                blob.UploadFromStream(stream);

                var newUrl = blob.Uri;

                return new Tuple<Uri, string, long>(newUrl, mediaType, length);
            }
        }
    }
}
