using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public IQueryable<User> GetAllUsers();
        public Task<User?> GetUserById(Guid id);
        public Task<User?> GetUserByEmail(string email);
        public Task<User> AddUser(User user);
        public Task<User> UpdateUser(User user);
        public Task DeleteUser(Guid id);
    }
}
