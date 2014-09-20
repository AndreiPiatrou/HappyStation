using System.Collections.Generic;
using System.Linq;

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
    }
}
