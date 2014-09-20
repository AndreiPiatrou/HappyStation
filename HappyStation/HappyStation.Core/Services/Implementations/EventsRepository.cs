using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class EventsRepository : RepositoryBase<Event>
    {
        public EventsRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}