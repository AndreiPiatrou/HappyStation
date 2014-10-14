using System.Linq;

namespace HappyStation.Web.ViewModels
{
    public class NewsViewModel : IViewModelBase
    {
        public int Id { get; set; }

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