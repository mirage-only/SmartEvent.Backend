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

        public async Task DeleteUser(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null) throw new Exception("User is not found");
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return dbContext.Users.AsNoTracking();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
