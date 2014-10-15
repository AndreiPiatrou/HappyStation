using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class User : DatabaseEntity
    {
        public string Photo { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        public string VkId { get; set; }

        public string FbId { get; set; }

        public string TwId { get; set; }
    }
}
