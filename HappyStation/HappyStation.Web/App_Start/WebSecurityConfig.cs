using WebMatrix.WebData;

namespace HappyStation.Web.App_Start
{
    public class WebSecurityConfig
    {
        public static void Configure()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Email", autoCreateTables: true);
        }
    }
}