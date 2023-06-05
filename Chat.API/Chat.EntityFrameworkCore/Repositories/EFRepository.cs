using Chat.Core.Helper;
using Chat.Domain.DBFilter;
using Chat.Domain.Repositories;
using Chat.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.EntityFrameworkCore.Repositories
{
    public class EFRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntitiy<TPrimaryKey>
    {
        protected readonly IDBContext _context;
        protected readonly EFDBContext _efContext;
        protected readonly DbSet<TEntity> _dbSet;

        internal EFRepository(IDBContext context)
        {
            this._context = context;
            this._efContext = this._context as EFDBContext;
            this._dbSet = this._efContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Table { get { return this._dbSet; } }

        public IQueryable<TEntity> GetAll()
        {
            return this._dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await this._dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public int Count()
        {
            return this.GetAll().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Count(predicate);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await this.GetAll().CountAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await this.GetAll().CountAsync(predicate, cancellationToken);
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Any(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            return result.Entity;
        }

        public IEnumerable<TEntity> BulkInsert(IEnumerable<TEntity> entities)
        {
            var bulkInsert = entities as TEntity[] ?? entities.ToArray();
            _dbSet.AddRange(bulkInsert);
            return bulkInsert;
        }

        public TEntity Update(TEntity entity)
        {
            TEntity local_Entity = _efContext.Set<TEntity>().Local.FirstOrDefault(p => p.Id.Equals(entity.Id));

            if (local_Entity != null)
            {
                var tempEntity = entity;
                entity = local_Entity;
                MapperHelper.Mapper<TEntity, TEntity>(tempEntity, entity);
            }

            if (this._efContext.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            this._efContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = this._dbSet.Where(predicate);
            foreach (var entity in entities)
            {
                this.Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {

            TEntity local_Entity = _efContext.Set<TEntity>().Local.FirstOrDefault(p => p.Id.Equals(entity.Id));
            if (local_Entity != null)
            {
                entity = local_Entity;
            }

            if (this._efContext.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }
            if (entity is IDeleteFilter)
            {
                (entity as IDeleteFilter).Deleted = true;
            }
            else
            {
                this._dbSet.Remove(entity);
            }

        }
    }

    public class EFRepository<TEntity> : EFRepository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntitiy<int>
    {
        internal EFRepository(IDBContext _context) : base(_context)
        {
        }
    }
}
