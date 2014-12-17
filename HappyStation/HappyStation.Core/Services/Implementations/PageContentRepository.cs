using System.Linq;

using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class PageContentRepository:RepositoryBase<PageContent>
    {
        public PageContentRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public PageContent GetByType(PageContentType type)
        {
            return Db.Set<PageContent>().FirstOrDefault(c => c.Type == type);
        }
    }
}