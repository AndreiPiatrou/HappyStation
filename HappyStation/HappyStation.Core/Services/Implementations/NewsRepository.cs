using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
