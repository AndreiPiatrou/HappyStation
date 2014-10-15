using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HappyStation.Web.ViewModels
{
    public class NewsViewModel : IViewModelBase
    {
        public int Id { get; set; }

        [UIHint("Html"), AllowHtml]
        public string Text { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

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