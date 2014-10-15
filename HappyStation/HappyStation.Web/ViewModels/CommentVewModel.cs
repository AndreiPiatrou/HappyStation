namespace HappyStation.Web.ViewModels
{
    public class CommentVewModel : IViewModelBase
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }
    }
}