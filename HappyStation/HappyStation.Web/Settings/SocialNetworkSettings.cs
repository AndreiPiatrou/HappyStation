namespace HappyStation.Web.Settings
{
    public class SocialNetworkSettings
    {
        public SocialNetworkSettings(string token, string clienId)
        {
            AccessToken = token;
            ClientId = clienId;
        }

        public string AccessToken { get; set; }

        public string ClientId { get; set; }
    }
}