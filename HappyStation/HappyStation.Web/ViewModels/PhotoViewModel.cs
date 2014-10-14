namespace HappyStation.Web.ViewModels
{
    public class PhotoViewModel : IViewModelBase
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public int PhotoAlbumId { get; set; }
    }
}