using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class RegistrationRepository(ApplicationDbContext dbContext) : IRegistrationRepository
    {
        public async Task<Registration> AddRegistration(Registration registration)
        {
            dbContext.Registrations.Add(registration);
            await dbContext.SaveChangesAsync();
            return registration;
        }

        public async Task<Registration?> GetRegistrationByEventIdAndUserId(Guid eventId, Guid userId)
        {
            var registration = await dbContext.Registrations
                .FirstOrDefaultAsync(reg => reg.EventId == eventId && reg.UserId == userId);
            
            return registration;
        }

        public Task DeleteRegistration(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registration> GetAllRegistrations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registration> GetAllRegistrationsByEventId(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<Registration?> GetRegistrationById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Registration> UpdateRegistration(Registration registration)
        {
            throw new NotImplementedException();
        }
    }
}
