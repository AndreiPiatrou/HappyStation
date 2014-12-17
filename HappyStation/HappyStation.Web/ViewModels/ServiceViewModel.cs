using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HappyStation.Web.ViewModels
{
    public class ServiceViewModel : IViewModelBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        [UIHint("Html"), AllowHtml]
        public string Description { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public bool IsHot { get; set; }

        public int? Order { get; set; }

        public string DescriptionPreview
        {
            get
            {
                return Description.Length > 200
                    ? Description.Take(200) + "..."
                    : Description;
            }
        }
    }
}