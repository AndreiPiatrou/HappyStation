using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class RepositoryBase<T> : IRepository<T> where T : DatabaseEntity
    {
        public RepositoryBase(DatabaseContext.DatabaseContext databaseContext)
        {
            Db = databaseContext;
        }

        protected DatabaseContext.DatabaseContext Db;

        public T CreateOrUpdate(T entity)
        {
            Contract.Requires(entity != null);

            Db.Set<T>().AddOrUpdate(entity);
            Db.SaveChanges();

            return entity;
        }

        public IEnumerable<T> CreateOrUpdate(IEnumerable<T> entities)
        {
            Contract.Requires(entities != null);

            var orUpdate = entities as T[] ?? entities.ToArray();
            foreach (var entity in orUpdate.ToList())
            {
                Db.Set<T>().AddOrUpdate(entity);
            }

            Db.SaveChanges();

            return orUpdate;
        }

        public bool Delete(int id)
        {
            Contract.Requires(id > 0);

            var entity = Db.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Db.Set<T>().Remove(entity);
            }
            Db.SaveChanges();

            return entity != null;
        }

        public virtual T Get(int id)
        {
            Contract.Requires(id > 0);

            return Db.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<T> GetBy(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Requires(take > 0);

            return Db.Set<T>().OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }

        public IEnumerable<T> GetRandom(int count = Numbers.MaxGetCount)
        {
            Contract.Requires(count > 0);

            return Db.Set<T>().OrderBy(e => Guid.NewGuid()).Take(count);
        }
    }
}