using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRememberer.Infrastructure.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        public BlobStorageService(IConfiguration configuration)
        {
            var azureConfig = configuration.GetSection("AzureStorage");
            string accountName = azureConfig["AccountName"]!;
            string accountKey = azureConfig["AccountKey"]!;
            string containerName = azureConfig["ContainerName"]!;

            string connectionString = azureConfig["ConnectionString"]!;

            _containerClient = new BlobContainerClient(connectionString, containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.Blob); // optional
        }

        public async Task UploadBlobAsync(string blobName, Stream data, string contentType)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(data, new BlobHttpHeaders { ContentType = contentType });
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}

