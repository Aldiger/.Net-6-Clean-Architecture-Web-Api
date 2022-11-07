using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace WebApi.Core.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable
       where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private IDbContextTransaction _transaction;
        private bool _disposed;
        public UnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OpenTransaction()
        {
            _transaction ??= _dbContext.Database.BeginTransaction();
        }


        public void Commit()
        {
            try
            {
                //_dbContext.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }

        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public Dictionary<Type, object> Repositories = new();
        public virtual TRepo Repository<TRepo>() where TRepo : IBaseRepository
        {
            var repositoryClass = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes()).FirstOrDefault(x => typeof(TRepo).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            if (repositoryClass == null)
            {
                throw new NotImplementedException("Requested repository not found!");
            }

            var entityType = ((Type[])((TypeInfo)repositoryClass).ImplementedInterfaces)[0]
            .GenericTypeArguments.FirstOrDefault();

            if (entityType == null)
            {
                throw new NotImplementedException("Requested repository not found!");
            }

            if (Repositories.Keys.Contains(entityType))
            {
                return (TRepo)Repositories[entityType];
            }

            var repo = default(TRepo);
            try
            {
                repo = (TRepo)Activator.CreateInstance(
                    repositoryClass, _dbContext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            Repositories.Add(entityType, repo);
            return repo;
        }

        #region IDisposable members
        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (this._transaction != null)
                {
                    this._transaction.Dispose();
                    this._transaction = null;
                }

                _dbContext.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}
