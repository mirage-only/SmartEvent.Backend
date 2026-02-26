using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IEventRepository
    {
        public IEnumerable<Event> GetAllEvents();
        public Task<Event?> GetEventById(Guid id);
        public Task<Event> AddEvent(Event @event);
        public Task<Event> UpdateEvent(Event @event);
        public Task DeleteEvent(Guid id);

    }
}
