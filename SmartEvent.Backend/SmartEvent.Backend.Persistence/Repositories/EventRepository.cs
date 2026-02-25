using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class EventRepository() : IEventRepository
    {
        public IEnumerable<Event> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public Task<Event?> GetEventById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> AddEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public Task<Event> UpdateEvent(Event @event)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEvent(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
