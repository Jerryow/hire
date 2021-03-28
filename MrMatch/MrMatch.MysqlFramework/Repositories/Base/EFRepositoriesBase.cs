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
    //*********此类不要使用************
    /// <summary>
    /// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.
    /// It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class EFRepositoriesBase<TEntity> : RepositoriesBase<TEntity> 
        where TEntity : Entity 
    {

        public DbSet<TEntity> query;

        private readonly MrMatchDbContext dbContext;
        public EFRepositoriesBase(MrMatchDbContext _dbContext)
        {
            dbContext = _dbContext;
            query = dbContext.Set<TEntity>();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return query;
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
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

        public override List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }
        
        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(long id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return query.Add(entity);
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(query.Add(entity));
        }

        public override long InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);
            dbContext.SaveChanges();
            return entity.PKID;
        }

        public override async Task<long> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.PKID;
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }


        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            query.Remove(entity);
        }

        public override void Delete(long id)
        {
            var entity = query.Local.FirstOrDefault(ent => EqualityComparer<long>.Default.Equals(ent.PKID, id));
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            Delete(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!query.Local.Contains(entity))
            {
                query.Attach(entity);
            }
        }
    }
}
