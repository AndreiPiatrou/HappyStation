using System;
using System.Configuration;

namespace HappyStation.Web.Settings
{
    public class ApplicaitonSettings
    {
        public int ItemsPerPage
        {
            get
            {
                return Read<int>(ItemsPerPageKey);
            }
        }

        private T Read<T>(string propertyName) where T : IConvertible
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[propertyName], typeof(T));
        }

        private const string ItemsPerPageKey = "ItemsPerPage";
    }
}