using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
