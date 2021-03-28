using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.EntityBase;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.MysqlFramework.BaseContext;

namespace MrMatch.MysqlFramework.Repositories.Base
{
    /// <summary>
    /// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.
    /// It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class ImpRepositoriesBase<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {

        public DbSet<TEntity> query;

        private readonly IDbContext dbContext;
        public ImpRepositoriesBase(IDbContext _dbContext)
        {
            dbContext = _dbContext;
            query = dbContext.Set<TEntity>();
        }

        #region Select/Get/Query
        public IQueryable<TEntity> GetAll()
        {
            return query;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors == null || propertySelectors.Count() <= 0)
            {
                return GetAll();
            }

            var query = GetAll();

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public TEntity Get(long id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(long id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public TEntity FirstOrDefault(long id)
        {
            return GetAll().FirstOrDefault(x => x.PKID == id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(long id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.PKID == id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        //public IQueryable<TEntity> GetByPagenation<TKey>(int pageIndex, int pageSize, out int total, out int totalCount, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderbyLambda, bool isAsc)
        //{
        //    total = GetAll().Where(whereLambda).Count();
        //    var count = total / pageSize;
        //    totalCount = total % pageSize == 0 ? count : count + 1;
        //    if (isAsc)
        //    {
        //        var result = GetAll().Where(whereLambda)
        //                     .OrderBy(orderbyLambda)
        //                     .Skip(pageSize * (pageIndex - 1))
        //                     .Take(pageSize);
        //        return result;
        //    }
        //    else
        //    {
        //        var result = GetAll().Where(whereLambda)
        //                   .OrderByDescending(orderbyLambda)
        //                   .Skip(pageSize * (pageIndex - 1))
        //                   .Take(pageSize).AsQueryable();
        //        return result;
        //    }
        //}

        public IQueryable<TEntity> GetByPagenation<TKey>(int pageIndex, int pageSize, out int total, out int totalCount, IQueryable<TEntity> entities, Expression<Func<TEntity, TKey>> orderbyLambda, bool isAsc)
        {
            total = entities.Count();
            var count = total / pageSize;
            totalCount = total % pageSize == 0 ? count : count + 1;
            if (isAsc)
            {
                var result = entities.OrderBy(orderbyLambda)
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize);
                return result;
            }
            else
            {
                var result = entities.OrderByDescending(orderbyLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize).AsQueryable();
                return result;
            }
        }
        #endregion


        #region Count
        public int Count()
        {
            return query.Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }
        #endregion
    }
}
