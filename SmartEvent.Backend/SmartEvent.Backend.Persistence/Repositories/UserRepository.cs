using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<User> AddUser(User user)
        { 
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public IQueryable<User> GetAllUsers() => dbContext.Users.AsNoTracking();

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
