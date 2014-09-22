using HappyStation.Core.Entities;

namespace HappyStation.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNew(this DatabaseEntity entity)
        {
            return entity.Id < 1;
        }
    }
}
