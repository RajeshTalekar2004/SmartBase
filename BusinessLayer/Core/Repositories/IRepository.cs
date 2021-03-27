using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        public TEntity Get(int id);

        public Task<TEntity> GetAsync(int id);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAll();
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

         IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        public Task Add(TEntity entity);

        public Task AddAsync(TEntity entity);

        public Task AddRangeAsync(IEnumerable<TEntity> entities);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
    }
}
