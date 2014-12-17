using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class Service : DatabaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public bool IsHot { get; set; }

        public int? Order { get; set; }
    }
}