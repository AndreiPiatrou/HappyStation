namespace HappyStation.Core.Entities
{
    public class PageContent : DatabaseEntity
    {
        public PageContentType Type { get; set; }

        public string Text { get; set; }
    }
}
