using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class RegistrationRepository() : IRegistrationRepository
    {
        public Task<Registration> AddRegistration(Registration registration)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRegistration(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Registration> GetAllRegistrations()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Registration> GetAllRegistrationsByEventId(Guid eventId)
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
