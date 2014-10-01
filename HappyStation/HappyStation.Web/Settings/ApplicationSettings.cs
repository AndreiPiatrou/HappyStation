using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;

using OAuth2;
using OAuth2.Client;

namespace HappyStation.Web.Settings
{
    public class ApplicationSettings
    {
        public ApplicationSettings(AuthorizationRoot root)
        {
            Contract.Requires(root != null);

            this.root = root;
            InitProperties();
        }

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

        public string UserInstagramAccessToken
        {
            get
            {
                return Read<string>(InstagramAccesstokenKey);
            }
        }

        public string InstagramUserId
        {
            get
            {
                return Read<string>(InstagramUserIdKey);
            }
        }

        public SocialNetworkSettings InstagramSettings
        {
            get
            {
                return instagramSettings;
            }
        }

        public string InstagramUserName
        {
            get
            {
                return Read<string>(InstagramUserNameKey);
            }
        }

        private T Read<T>(string propertyName) where T : IConvertible
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[propertyName], typeof(T));
        }

        private void InitProperties()
        {
            var client = GetClient("Instagram");
            instagramSettings = new SocialNetworkSettings(client.Configuration.ClientPublic,
                client.Configuration.ClientId);
        }

        private IClient GetClient(string networName)
        {
            return root.Clients.First(c => c.Name.ToLower() == networName.ToLower());
        }

        private const string ItemsPerPageKey = "ItemsPerPage";
        private const string FileUploadPathKey = "FileUploadPath";
        private const string InstagramAccesstokenKey = "InstagramAccesstoken";
        private const string InstagramUserIdKey = "InstagramUserId";
        private const string InstagramUserNameKey = "InstagramUserName";

        private readonly AuthorizationRoot root;
        private SocialNetworkSettings instagramSettings;
    }
}