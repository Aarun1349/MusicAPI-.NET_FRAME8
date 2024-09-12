using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
namespace MusicAPI.Helpers
{
    public static class FileUpload
    {
        public static async Task<string> ImageUpload(IFormFile file) {
            string connectonString = "";
            string containerName = "";
            BlobContainerClient blobClientContainer = new BlobContainerClient(connectonString, containerName);
            BlobClient blobClient  = blobClientContainer.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;

        }
        public static async Task<string> AudioUpload(IFormFile audio)
        {
            string connectonString = "";
            string containerName = "";
            BlobContainerClient blobClientContainer = new BlobContainerClient(connectonString, containerName);
            BlobClient blobClient = blobClientContainer.GetBlobClient(audio.FileName);
            var memoryStream = new MemoryStream();
            await audio.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;


        }
    }
}
