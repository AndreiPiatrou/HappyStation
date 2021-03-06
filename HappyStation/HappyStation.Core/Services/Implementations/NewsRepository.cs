﻿using System.Collections.Generic;
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
            Contract.Requires(count > 0);

            return Db.News.Where(n => n.Type == NewsType.Handmade).OrderByDescending(n => n.Id).Take(count);
        }

        public IEnumerable<News> GetPortfolioOnly(int skip = 0, int take = Numbers.MaxGetCount)
        {
            return Db.News.Where(n => n.Type == NewsType.Portfolio).OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }

        public IEnumerable<News> GetActions(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Requires(take > 0);

            return Db.News.Where(n => n.Type == NewsType.Action).OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }

        public IEnumerable<News> GetNewsOnly(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Requires(take > 0);

            return Db.News.Where(n => n.Type == NewsType.News).OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }

        public News Get(string id)
        {
            int realId;
            return int.TryParse(id, out realId) ? Get(realId) : Db.News.FirstOrDefault(n => n.Alias == id);
        }
    }
}
