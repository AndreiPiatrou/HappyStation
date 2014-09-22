using System;
using System.Configuration;

namespace HappyStation.Web.Settings
{
    public class ApplicationSettings
    {
        public int ItemsPerPage
        {
            get
            {
                return Read<int>(ItemsPerPageKey);
            }
        }

        public string FileUploadPath
        {
            get
            {
                return Read<string>(FileUploadPathKey);
            }
        }

        private T Read<T>(string propertyName) where T : IConvertible
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[propertyName], typeof(T));
        }

        private const string ItemsPerPageKey = "ItemsPerPage";
        private const string FileUploadPathKey = "FileUploadPath";
    }
}