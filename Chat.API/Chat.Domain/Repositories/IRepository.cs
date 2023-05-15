using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey>
    {
        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(CancellationToken cancellationToken);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        bool Contains(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        void Delete(TEntity entity);

        IEnumerable<TEntity> BulkInsert(IEnumerable<TEntity> entities);
    }
    public interface IRepository<TEntity> : IRepository<TEntity, int>
    {

    }
}
