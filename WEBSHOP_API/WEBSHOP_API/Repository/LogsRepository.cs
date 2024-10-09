using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class LogsRepository : ILogsRepository, IDisposable
    {

        private WebshopDbContext context;
        public LogsRepository(WebshopDbContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<StorageLogger>> GetLogsByAccoutId(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<StorageLogger> GetLogsById(int logId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StorageLogger>> GetLogsByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
