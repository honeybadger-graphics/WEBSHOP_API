using WEBSHOP_API.Database;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository.RepositoryInterface;

namespace WEBSHOP_API.Repository
{
    public class UserDataRepository: IUserDataRepository, IDisposable
    {
        private WebshopDbContext _context;
        public UserDataRepository(WebshopDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<UserData> GetUserDataById(string uId)
        {
           return await _context.UserDatas.FindAsync(uId);
        }

        public async Task CreateUserData(UserData userData)
        {
            await _context.UserDatas.AddAsync(userData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserData(UserData userData)
        {
            var existingUser = await _context.UserDatas.FindAsync(userData.UserId);
            _context.Entry(existingUser).CurrentValues.SetValues(userData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserData(string uId)
        {
            var existingUser = await _context.UserDatas.FindAsync(uId);
            _context.UserDatas.Remove(existingUser);
            _context.SaveChanges();

        }
    }
}
