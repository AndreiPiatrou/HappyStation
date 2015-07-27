using System.ComponentModel.DataAnnotations;
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

        [AllowHtml, Required]
        public string Description { get; set; }

        public string Alias { get; set; }

        public string Url
        {
            get
            {
                return string.IsNullOrEmpty(Alias) ? Id.ToString() : Alias;
            }
        }
    }
}