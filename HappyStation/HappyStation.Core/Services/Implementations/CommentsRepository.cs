using System.Linq;

using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class CommentsRepository : RepositoryBase<Comment>
    {
        public CommentsRepository(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public Comment GetLast()
        {
            return Db.Comments.AsEnumerable().LastOrDefault();
        }
    }
}
