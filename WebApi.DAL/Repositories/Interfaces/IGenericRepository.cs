namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Disabled
        /*
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IEnumerable<TEntity> GetAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetFirstAsNoTracking(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        */
        #endregion
        Task<TEntity> GetByIDAsync(object id);
        TEntity GetByID(object id);
        Task<TEntity> GetByIDAsNoTrackingAsync(object id, int? cacheDuration = null);
        TEntity GetByIDAsNoTracking(object id, int? cacheDuration = null);
        Task<IEnumerable<TEntity>> GetAsync();
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(int? cacheDuration = null);
        IEnumerable<TEntity> GetAsNoTracking(int? cacheDuration = null);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);
        void Update(TEntity entityToUpdate);
        void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    }
}