using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class Repository<T> : IRepository<T> where T : DatabaseEntity
    {
        public Repository(DatabaseContext.DatabaseContext databaseContext)
        {
            Db = databaseContext;
        }

        protected DatabaseContext.DatabaseContext Db;

        public T CreateOrUpdate(T entity)
        {
            Contract.Ensures(entity != null);

            Db.Set<T>().AddOrUpdate(entity);
            Db.SaveChanges();

            return entity;
        }

        public bool Delete(int id)
        {
            Contract.Ensures(id > 0);

            var entity = Db.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Db.Set<T>().Remove(entity);
            }

            return entity != null;
        }

        public T Get(int id)
        {
            Contract.Ensures(id > 0);

            return Db.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetBy(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Ensures(take > 0);

            return Db.Set<T>().OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }
    }
}