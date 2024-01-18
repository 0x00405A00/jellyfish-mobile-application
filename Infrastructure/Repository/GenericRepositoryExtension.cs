using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public static class GenericRepositoryExtension
    {

        public static IQueryable<TEntity> ExpressionQuery<TEntity>(this DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> expression = null)
            where TEntity : Entity
        {
            var value = expression == null ?
                dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().AsQueryable().Where(expression);
            return value;
        }

    }
}
