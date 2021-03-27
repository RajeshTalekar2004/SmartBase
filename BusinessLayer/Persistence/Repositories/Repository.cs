using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        async Task IRepository<TEntity>.AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }


         async Task IRepository<TEntity>.Add(TEntity entity)
        {
           await Context.Set<TEntity>().AddAsync(entity);
        }


         async Task<TEntity> IRepository<TEntity>.GetAsync(int id)
        {

            return await Context.Set<TEntity>().FindAsync(id);
        }


        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        async Task<IEnumerable<TEntity>> IRepository<TEntity>.GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }


        async Task<IEnumerable<TEntity>> IRepository<TEntity>.FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        async Task<TEntity> IRepository<TEntity>.SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }


        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }


        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
           return Context.Set<TEntity>().AddRangeAsync(entities);
        }


        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }




    }
}
