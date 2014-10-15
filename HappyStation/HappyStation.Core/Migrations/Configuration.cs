using System.Linq;

using HappyStation.Core.Entities;

using WebMatrix.WebData;

namespace HappyStation.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext.DatabaseContext context)
        {
            WebSecurityConfig.Initialize();

            AddUsers(context);
        }

        private void AddUsers(DatabaseContext.DatabaseContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            context.Users.Add(new User
            {
                Email = "19graff91@gmail.com",
            });

            context.Users.Add(new User
            {
                Email = "admin@admin.com",
            });

            context.SaveChanges();

            WebSecurity.CreateAccount("19graff91@gmail.com", "19happymoments91");
            WebSecurity.CreateAccount("admin@admin.com", "19admin91");
        }
    }
}
