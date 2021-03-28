using MrMatch.Domain.EntityBase.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.MysqlFramework.BaseContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext dbContext;

        public UnitOfWork(IDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public bool Commit()
        {
            return dbContext.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public TEntity RegisterNewEntity<TEntity>(TEntity entity) where TEntity : class
        {
            return dbContext.Set<TEntity>().Add(entity);
        }

        public void RegisterUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
