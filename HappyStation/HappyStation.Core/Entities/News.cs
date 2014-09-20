namespace HappyStation.Core.Entities
{
    public class News : DatabaseEntity
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }
    }
}
