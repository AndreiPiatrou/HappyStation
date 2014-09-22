using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNew(this IViewModelBase entity)
        {
            return entity.Id < 1;
        }
    }
}