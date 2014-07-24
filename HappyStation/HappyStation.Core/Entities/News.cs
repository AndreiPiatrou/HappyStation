using System.Collections.Generic;

namespace HappyStation.Core.Entities
{
    public class News : DatabaseEntity
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
