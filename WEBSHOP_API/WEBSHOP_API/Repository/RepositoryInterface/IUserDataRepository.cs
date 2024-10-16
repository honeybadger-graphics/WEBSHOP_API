using WEBSHOP_API.Models;

namespace WEBSHOP_API.Repository.RepositoryInterface
{
    public interface IUserDataRepository : IDisposable
    {
        Task<UserData> GetUserDataById(string uId); 
        Task CreateUserData(UserData userData);
        Task UpdateUserData(UserData userData);
        Task DeleteUserData(string uId);
        void Save();
    }
}
