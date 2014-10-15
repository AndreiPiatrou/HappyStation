using System.Linq;

using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public User GetByLogin(string login)
        {
            return Db.Users.FirstOrDefault(u => u.Email == login);
        }
    }
}
