using WebMatrix.WebData;

namespace HappyStation.Core.Migrations
{
    public static class WebSecurityConfig
    {
        public static void Initialize()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection(
                    "DefaultConnection",
                    "Users",
                    "Id",
                    "Email",
                    autoCreateTables: true);
            }
        }
    }
}