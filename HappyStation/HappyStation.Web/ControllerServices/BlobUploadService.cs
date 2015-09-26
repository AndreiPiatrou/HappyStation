using System;
using System.Configuration;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using RestSharp.Extensions;

namespace HappyStation.Web.ControllerServices
{
    public class BlobUploadService : IFileUploadService
    {
        public string SaveImage(HttpPostedFileBase fileToSave, HttpContextBase context)
        {
            var fileName = Guid.NewGuid() + "_" +  fileToSave.FileName;
            var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");

            container.CreateIfNotExistsAsync();

            var blockBlob = container.GetBlockBlobReference(fileName);

            var bytes = fileToSave.InputStream.ReadAsBytes();
            blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            return container.Uri + "/" + fileName;
        }

        public bool DeleteFile(string fileName)
        {
            var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");

            var blockBlob = container.GetBlockBlobReference(fileName);

            return blockBlob.DeleteIfExists();
        }
    }
}
