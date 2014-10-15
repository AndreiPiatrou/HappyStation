using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class Comment : DatabaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsAccepted { get; set; }
    }
}
