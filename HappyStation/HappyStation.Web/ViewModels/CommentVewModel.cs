using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HappyStation.Web.ViewModels
{
    public class CommentVewModel : IViewModelBase
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [UIHint("Html"), AllowHtml]
        public string Text { get; set; }

        public bool IsAccepted { get; set; }
    }
}