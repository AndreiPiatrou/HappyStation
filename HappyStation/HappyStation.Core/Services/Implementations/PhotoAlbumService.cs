using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;

namespace HappyStation.Core.Services.Implementations
{
    public class PhotoAlbumService : RepositoryBase<PhotoAlbum>
    {
        public PhotoAlbumService(DatabaseContext.DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public override PhotoAlbum Get(int id)
        {
            return Db.PhotoAlbums.Include("Photos").FirstOrDefault(pa => pa.Id == id);
        }

        public override IEnumerable<PhotoAlbum> GetBy(int skip = 0, int take = Numbers.MaxGetCount)
        {
            Contract.Ensures(take > 0);

            return Db.Set<PhotoAlbum>().Include("Photos").OrderByDescending(e => e.CreatedAt).Skip(skip).Take(take);
        }
    }
}