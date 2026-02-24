using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class EventOrganizerRepository() : IEventOrganizerRepository
    {
        public Task<EventOrganizer> AddEventOrganizer(EventOrganizer eventOrganizer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEventOrganizer(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventOrganizer> GetAllEventOrganizers()
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventOrganizer> GetAllEventOrganizersByEventId(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<EventOrganizer?> GetEventOrganizerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EventOrganizer> UpdateEventOrganizer(EventOrganizer eventOrganizer)
        {
            throw new NotImplementedException();
        }
    }
}
