using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class Photo : DatabaseEntity
    {
        [Required]
        public string Path { get; set; }

        public string Title { get; set; }

        // TODO: Add entity reference.
        public IEnumerable<Comment> Comments { get; set; }

        [Required]
        public PhotoAlbum Album { get; set; }
    }
}