namespace HappyStation.Web.ViewModels
{
    public class ServiceViewModel
    {
        public ServiceViewModel()
        {
        }

        public ServiceViewModel(string title, string shortDescription, string image, int price)
        {
            Title = title;
            ShortDescription = shortDescription;
            Image = image;
            Price = price;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public bool IsHot { get; set; }
    }
}