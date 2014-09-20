namespace HappyStation.Core.Entities
{
    public class Service : DatabaseEntity
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public bool IsHot { get; set; }
    }
}