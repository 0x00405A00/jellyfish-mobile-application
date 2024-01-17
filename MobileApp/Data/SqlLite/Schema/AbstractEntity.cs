using System.Reflection;

namespace MobileApp.Data.SqlLite.Schema
{
    public class AbstractEntity
    {
    }
    public static class AbstractEntitiyExtension
    {
        public static string GetTableName(this AbstractEntity entity)
        {
            return entity.GetType().GetCustomAttributes<SQLite.TableAttribute>()?.First().Name;
        }
    }
}
