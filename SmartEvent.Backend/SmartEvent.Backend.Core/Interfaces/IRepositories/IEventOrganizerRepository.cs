using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IEventOrganizerRepository
    {
        public IEnumerable<EventOrganizer> GetAllEventOrganizers();
        public IEnumerable<EventOrganizer> GetAllEventOrganizersByEventId(Guid eventId);
        public Task<EventOrganizer?> GetEventOrganizerById(Guid id);
        public Task<EventOrganizer> AddEventOrganizer(EventOrganizer eventOrganizer);
        public Task<EventOrganizer> UpdateEventOrganizer(EventOrganizer eventOrganizer);
        public Task DeleteEventOrganizer(Guid id);

    }
}
