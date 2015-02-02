using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Web.ViewModels
{
    public class PhotoAlbumViewModel : IViewModelBase
    {
        public PhotoAlbumViewModel()
        {
            Photos = new List<PhotoViewModel>();
        }

        [Required]
        public string Title { get; set; }

        public int Id { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }
    }
}