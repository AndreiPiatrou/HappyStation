using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Web;

using HappyStation.Web.Settings;

namespace HappyStation.Web.ControllerServices
{
    public class FileUploadService
    {
        public FileUploadService(ApplicationSettings settings)
        {
            Contract.Requires(settings != null);

            rootFilePath = settings.FileUploadPath;
        }

        public string SaveImage(HttpPostedFileBase fileToSave, HttpContextBase context)
        {
            Contract.Requires(fileToSave != null);

            var data = new byte[fileToSave.ContentLength];
            fileToSave.InputStream.Read(data, 0, fileToSave.ContentLength);

            var filename = GenerateFileName();
            var filePathOriginal = context.Server.MapPath(rootFilePath);
            CheckDirectoryExisting(filePathOriginal);

            string savedFileName = Path.Combine(filePathOriginal, filename);
            fileToSave.SaveAs(savedFileName);

            return Path.Combine(rootFilePath, filename);
        }

        public bool DeleteFile(string fileName)
        {
            Contract.Requires(!string.IsNullOrEmpty(fileName));

            try
            {
                if (!File.Exists(fileName))
                {
                    return false;
                }

                File.Delete(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateFileName()
        {
            return Guid.NewGuid() + ".jpg";
        }

        private void CheckDirectoryExisting(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }


        private readonly string rootFilePath;
    }
}