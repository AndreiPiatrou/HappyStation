using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class EventsRepository : Repository<Event>
    {
        public EventsRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}