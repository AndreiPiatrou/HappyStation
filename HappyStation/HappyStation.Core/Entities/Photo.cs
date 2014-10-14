using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class Photo : DatabaseEntity
    {
        [Required]
        public string Path { get; set; }

        public string Title { get; set; }
        
        [Required]
        public PhotoAlbum Album { get; set; }
    }
}