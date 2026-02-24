using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IAttendanceRepository
    {
        public IQueryable<Attendance> GetAllAttendances();
        public IQueryable<Attendance> GetAllAttendancesByUserId(Guid userId);
        public IQueryable<Attendance> GetAllAttendancesByEventId(Guid eventId);
        public Task<Attendance?> GetAttendanceById(Guid id);
        public Task<Attendance> AddAttendance(Attendance attendance);
        public Task<Attendance> UpdateAttendance(Attendance attendance);
        public Task DeleteAttendance(Guid id);
    }
}
