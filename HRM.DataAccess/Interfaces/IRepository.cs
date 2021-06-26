using System;
using System.Collections.Generic;

namespace HRM.DataAccessEf.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        (int totalCount, IEnumerable<TEntity> items) Get(int? pageIndex = null, int? pageSize = null, Func<TEntity, bool>? predicate = null);
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? predicate = null);
        void Update(TEntity obj);
        void Add(TEntity obj);
        void Remove(TEntity obj);
    }
}