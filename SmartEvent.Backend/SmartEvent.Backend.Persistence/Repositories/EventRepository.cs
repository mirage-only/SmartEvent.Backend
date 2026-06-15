using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class EventRepository(ApplicationDbContext dbContext) : IEventRepository
    {
        public IQueryable<Event> GetAllEvents() => dbContext.Events.AsNoTracking();

        public async Task<Event?> GetEventById(Guid id) => 
            await dbContext.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(@event => @event.Id == id);

        public async Task<List<Event>> GetEventsByUserIdAsync(Guid userId)
        {
            return await dbContext.Events
                .AsNoTracking()
                .Where(e => e.Registrations.Any(r => r.UserId == userId))
                .ToListAsync();
        }

        public async Task<Event?> GetEventWithQrCodeByIdAsync(Guid id) => 
            await dbContext.Events
                .AsNoTracking()
                .Include(@event => @event.PastQrCodes)
                .FirstOrDefaultAsync(@event => @event.Id == id);

        public async Task<Event?> GetEventForAttendanceAsync(Guid id) => 
            await dbContext.Events
                .AsNoTracking()
                .Include(@event => @event.Registrations)
                .Include(@event => @event.Attendances)
                .Include(@event => @event.PastQrCodes)
                .Include(@event => @event.Organizers)
                .FirstOrDefaultAsync(@event => @event.Id == id);

        public async Task<Event> AddEvent(Event @event)
        {
            await  dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();
            return @event;
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
