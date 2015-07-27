using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class HandMadesService : RepositoryBase<HandMade>
    {
        public HandMadesService(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
