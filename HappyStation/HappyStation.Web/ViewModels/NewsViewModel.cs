using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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

        [AllowHtml]
        public string ShortText
        {
            get
            {
                var text = StripTagsRegex(Text);
                return text.Length > 80
                    ? text.Substring(0, 79) + "..."
                    : text;
            }
        }

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}