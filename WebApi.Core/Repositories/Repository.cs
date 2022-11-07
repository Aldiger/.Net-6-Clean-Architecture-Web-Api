using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Entities;

namespace WebApi.Core.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IEntity
      where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public virtual TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            //.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public virtual TEntity Update(TEntity entity)
        {
            //Context.Entry(entity).State = EntityState.Modified;
            _context.Update(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
            return entities;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public virtual List<TEntity> Delete(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return entities;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }
            return query.AsNoTracking().AsQueryable().FirstOrDefault(expression);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }
            return await query.AsNoTracking().AsQueryable().FirstOrDefaultAsync(expression);
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take != 0)
            {
                query = query.Skip(skip).Take(take);
            }

            return query.ToList();

            //return expression == null 
            //    ? Context.Set<TEntity>().Skip(skip).Take(take != 0 ? take : int.MaxValue).AsNoTracking() 
            //    : Context.Set<TEntity>().Where(expression).Skip(skip).Take(take != 0 ? take : int.MaxValue).AsNoTracking();
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int skip = 0, int take = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take != 0)
            {
                query = query.Skip(skip).Take(take);
            }

            return await query.ToListAsync();
            //return expression == null ? await Context.Set<TEntity>().Skip(skip).Take(take != 0 ? take : int.MaxValue).AsNoTracking().ToListAsync() :
            //     await Context.Set<TEntity>().Where(expression).Skip(skip).Take(take != 0 ? take : int.MaxValue).AsNoTracking().ToListAsync();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public virtual async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await _context.Set<TEntity>().LongCountAsync()
                : await _context.Set<TEntity>().LongCountAsync(expression);
        }

        public virtual long GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? _context.Set<TEntity>().LongCount() : _context.Set<TEntity>().LongCount(expression);
        }
    }
}
