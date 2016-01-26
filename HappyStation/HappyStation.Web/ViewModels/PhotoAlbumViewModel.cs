using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [UIHint("Html"), AllowHtml]
        public string Text { get; set; }

        public int Id { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }

        public string Keywords { get; set; }
    }
}