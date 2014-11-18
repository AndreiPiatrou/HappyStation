using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using HappyStation.Core.Entities;

namespace HappyStation.Web.ViewModels
{
    public class NewsViewModel : IViewModelBase
    {
        public int Id { get; set; }

        [UIHint("Html"), AllowHtml, Required]
        public string Text { get; set; }

        [Required]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required]
        public NewsType Type { get; set; }

        public string ShortText
        {
            get
            {
                return Text.Length > 200
                    ? Text.Take(200) + "..."
                    : Text;
            }
        }
    }
}