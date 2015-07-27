using System.Collections.Generic;
using System.Linq;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class ServicesRepository : RepositoryBase<Service>
    {
        public ServicesRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<Service> GetHottest(int count = 4)
        {
            return Db.Services.Where(s => s.IsHot).Take(count);
        }

        public override IEnumerable<Service> GetBy(int skip = 0, int take = Numbers.MaxGetCount)
        {
            return base.GetBy(skip, take).OrderBy(s => s.Order);
        }

        public object Get(string id)
        {
            int realId;

            return int.TryParse(id, out realId) ? Get(realId) : Db.Services.FirstOrDefault(s => s.Alias == id);
        }
    }
}
