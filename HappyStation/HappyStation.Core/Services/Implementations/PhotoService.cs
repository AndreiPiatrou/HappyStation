using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class PhotoService : RepositoryBase<Photo>
    {
        public PhotoService(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
