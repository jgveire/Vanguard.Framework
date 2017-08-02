using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Vanguard.Framework.Website.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        TEntity GetById(params object[] id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
