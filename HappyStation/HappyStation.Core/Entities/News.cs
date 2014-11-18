using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class News : DatabaseEntity
    {
        public News()
        {
            Type = NewsType.News;
        }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [DefaultValue(NewsType.News)]
        public NewsType Type { get; set; }
    }
}
