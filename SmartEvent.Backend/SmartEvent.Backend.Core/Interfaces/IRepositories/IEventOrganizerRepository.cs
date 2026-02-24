using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IEventOrganizerRepository
    {
        public IQueryable<EventOrganizer> GetAllEventOrganizers();
        public IQueryable<EventOrganizer> GetAllEventOrganizersByEventId(Guid eventId);
        public Task<EventOrganizer?> GetEventOrganizerById(Guid id);
        public Task<EventOrganizer> AddEventOrganizer(EventOrganizer eventOrganizer);
        public Task<EventOrganizer> UpdateEventOrganizer(EventOrganizer eventOrganizer);
        public Task DeleteEventOrganizer(Guid id);

    }
}
