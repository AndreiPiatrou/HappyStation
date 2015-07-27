using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using HappyStation.Core.Entities;

namespace HappyStation.Web.ViewModels
{
    public class PageContentViewModel : IViewModelBase
    {
        public int Id { get; set; }

        [UIHint("Html"), AllowHtml, Required]
        public string Text { get; set; }

        public PageContentType Type { get; set; }
    }
}