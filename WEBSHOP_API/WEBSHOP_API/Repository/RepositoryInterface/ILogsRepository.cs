using WEBSHOP_API.Models;

namespace WEBSHOP_API.Repository.RepositoryInterface
{
    public interface ILogsRepository: IDisposable
    {

        Task<StorageLogger> GetLogsById(int logId);
        Task<IEnumerable<StorageLogger>> GetLogsByProductId(int productId);
        Task<IEnumerable<StorageLogger>> GetLogsByAccoutId(int accountId);
        void Save();
    }
}
