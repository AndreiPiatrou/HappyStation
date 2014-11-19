using System.Diagnostics.Contracts;

using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNew(this IViewModelBase entity)
        {
            Contract.Requires(entity != null);

            return entity.Id < 1;
        }
    }
}