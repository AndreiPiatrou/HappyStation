using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class User : DatabaseEntity
    {
        public string Photo { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string VkId { get; set; }

        public string FbId { get; set; }

        public string TwId { get; set; }
    }
}
