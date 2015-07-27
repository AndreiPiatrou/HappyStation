using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class ContentImagesService : RepositoryBase<ContentImage>
    {
        public ContentImagesService(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}