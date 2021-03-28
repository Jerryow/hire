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
    //*********此类测试专用************
    /// <summary>
    /// Base class to implement <see cref="IRepository{TEntity,TPrimaryKey}"/>.
    /// It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class EFRepositoriesBaseTest<TEntity> : IRepositoryTest<TEntity> 
        where TEntity : Entity 
    {

        public DbSet<TEntity> query;

        private readonly IDbContext dbContext;
        public EFRepositoriesBaseTest(IDbContext _dbContext)
        {
            dbContext = _dbContext;
            query = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return query;
        }

        public List<TEntity> GetAllList()
        {
            return query.ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            return query.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.query.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public int Count()
        {
            return query.Count();
        }
    }
}
