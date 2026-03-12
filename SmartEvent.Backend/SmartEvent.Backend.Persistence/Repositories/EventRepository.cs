using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class EventRepository(ApplicationDbContext dbContext) : IEventRepository
    {
        public IQueryable<Event> GetAllEvents() => dbContext.Events.AsNoTracking();

        public Task<Event?> GetEventById(Guid id) => dbContext.Events.AsNoTracking()
            .FirstOrDefaultAsync(@event => @event.Id == id);

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
