using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TableStorage.Repository.Abstract
{
    public interface ITableStorageRepository<TEntity> where TEntity : TableEntity, new()
    {
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> DeleteAsync(string rowKey, string partitionKey);
        Task<TEntity> GetAsync(string rowKey, string partitionKey);
        IQueryable<TEntity> QueryAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> QueryAsync();
    }
}
