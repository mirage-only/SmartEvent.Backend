using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) throw new Exception("User is not found");
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAllUsers()
        {
            return _dbContext.Users.AsQueryable();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
