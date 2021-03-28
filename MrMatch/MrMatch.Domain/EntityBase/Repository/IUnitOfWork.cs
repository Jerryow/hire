using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Domain.EntityBase.Repository
{
    public interface IUnitOfWork
    {
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;
        TEntity RegisterNewEntity<TEntity>(TEntity entity) where TEntity : class;
        void RegisterUpdate<TEntity>(TEntity entity) where TEntity : class;
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;
        bool Commit();

        Task<bool> CommitAsync();
    }
}
