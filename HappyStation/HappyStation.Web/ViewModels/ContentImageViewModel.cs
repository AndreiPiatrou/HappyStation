using System.Linq;

namespace HappyStation.Web.ViewModels
{
    public class ContentImageViewModel : IViewModelBase
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public bool IsImageFile
        {
            get
            {
                var extension = System.IO.Path.GetExtension(Path);
                return extension != null && imageFileextensions.Contains(extension.ToLower());
            }
        }

        private readonly string[] imageFileextensions =
        {
            ".jpg",
            ".png",
            ".jpeg",
            ".ico"
        };
    }
}