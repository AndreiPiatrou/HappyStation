using System.Collections.Generic;

namespace HappyStation.Web.ViewModels
{
    public class PhotoAlbumViewModel : IViewModelBase
    {
        public PhotoAlbumViewModel()
        {
            Photos = new List<PhotoViewModel>();
        }

        public string Title { get; set; }

        public int Id { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }
    }
}