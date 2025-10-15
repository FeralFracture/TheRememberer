namespace TheRememberer.Objects.Interfaces.Services
{
    public interface IBlobStorageService
    {
        public Task UploadBlobAsync(string blobName, Stream data, string contentType);
        public Task<Stream> DownloadBlobAsync(string blobName);
        public Task DeleteBlobAsync(string blobName);
    }
}

