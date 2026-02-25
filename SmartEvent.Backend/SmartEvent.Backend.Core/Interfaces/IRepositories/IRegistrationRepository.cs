using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IRegistrationRepository
    {
        public IEnumerable<Registration> GetAllRegistrations();
        public IEnumerable<Registration> GetAllRegistrationsByEventId(Guid eventId);
        public Task<Registration?> GetRegistrationById(Guid id);
        public Task<Registration> AddRegistration(Registration registration);
        public Task<Registration> UpdateRegistration(Registration registration);
        public Task DeleteRegistration(Guid id);
    }
}
