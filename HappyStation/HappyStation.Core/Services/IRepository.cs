using System.Collections.Generic;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services
{
    public interface IRepository<T> where T : DatabaseEntity
    {
        T CreateOrUpdate(T entity);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetBy(int skip = 0, int take = Numbers.MaxGetCount);
        IEnumerable<T> GetRandom(int count = Numbers.MaxGetCount);
    }
}