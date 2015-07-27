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

        public string ShortText
        {
            get
            {
                if (!string.IsNullOrEmpty(Text) && Text.Length > 500)
                {
                    return Text.Substring(0, 499) + "...";
                }

                return Text;
            }
        }
    }
}