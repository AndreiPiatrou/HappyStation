using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
