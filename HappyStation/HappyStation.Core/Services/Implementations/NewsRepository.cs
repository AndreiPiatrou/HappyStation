using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class NewsRepository : RepositoryBase<News>
    {
        public NewsRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<News> GetHottest(int count = 4)
        {
            return Db.News.Where(n => n.Type == NewsType.News).OrderByDescending(n => n.Id).Take(count);
        }

        public IEnumerable<News> GetLastHandMade(int count = 20)
        {
            return Db.News.Where(n => n.Type == NewsType.Handmade).OrderByDescending(n => n.Id).Take(count);
        }

        public IEnumerable<News> GetNewsOnly(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Requires(take > 0);

            return Db.News.Where(n => n.Type == NewsType.News).OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }
    }
}
