using System.Collections.Generic;

namespace HappyStation.Core.Entities
{
    public class PhotoAlbum : DatabaseEntity
    {
        public List<Photo> Photos { get; set; }

        public string Title { get; set; }
    }
}