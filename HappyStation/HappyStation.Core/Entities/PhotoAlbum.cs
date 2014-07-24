using System.Collections.Generic;

namespace HappyStation.Core.Entities
{
    public class PhotoAlbum : DatabaseEntity
    {
        // TODO: Add entity reference.
        public IEnumerable<Photo> Photos { get; set; }

        public string Title { get; set; }
    }
}