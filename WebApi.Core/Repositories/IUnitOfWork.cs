namespace WebApi.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void OpenTransaction();
        void Commit();
        void Rollback();
        /// <summary>
        /// Saves modified entities to db
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Saves modified entities to db
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        TRepo Repository<TRepo>() where TRepo : IBaseRepository;
    }
}
