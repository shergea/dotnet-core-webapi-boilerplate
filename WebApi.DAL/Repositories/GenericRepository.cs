using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebApi.DAL.Repositories.Interfaces;

namespace WebApi.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal MsSQLContext context;
        internal DbSet<TEntity> dbSet;
        private IDistributedCache distributedCache;

        public GenericRepository(MsSQLContext context, IDistributedCache distributedCache = null)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.distributedCache = distributedCache;
        }
        #region Disabled
        /* public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetFirstAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }
        */
        #endregion

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await dbSet.ToListAsync();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return dbSet.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(int? cacheDuration = null)
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAsNoTracking(int? cacheDuration = null)
        {
            IEnumerable<TEntity> response = null;
            if (cacheDuration != null)
            {
                var data = this.distributedCache.GetString(typeof(TEntity).Name + "æ" + "GetAsNoTracking");
                if (data != null)
                {
                    response = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(data);
                }

                //Console.WriteLine(typeof(TEntity).Name + "æ" + "GetAsNoTracking" + "   Çağırıldı.");
                if (response == null)
                {
                    var cacheExpirationOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(Convert.ToDouble(cacheDuration))
                    };
                    response = dbSet.AsNoTracking().ToList();

                    this.distributedCache.SetString(typeof(TEntity).Name + "æ" + "GetAsNoTracking", JsonConvert.SerializeObject(response), cacheExpirationOptions);
                    //Console.WriteLine(typeof(TEntity) + "æ" + "Get" + "   Set Edildi.");
                }
            }
            else
            {
                response = dbSet.AsNoTracking().ToList();
            }

            return response;
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIDAsNoTrackingAsync(object id, int? cacheDuration = null)
        {
            var entity = await dbSet.FindAsync(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual TEntity GetByIDAsNoTracking(object id, int? cacheDuration = null)
        {
            var entity = dbSet.Find(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entitiesToDelete)
        {
            foreach (TEntity entity in entitiesToDelete)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
            }
            dbSet.RemoveRange(entitiesToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            foreach (TEntity entity in entitiesToUpdate)
            {
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}