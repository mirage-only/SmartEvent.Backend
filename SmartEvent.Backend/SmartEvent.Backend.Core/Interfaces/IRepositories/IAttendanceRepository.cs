using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IAttendanceRepository
    {
        public IEnumerable<Attendance> GetAllAttendances();
        public IEnumerable<Attendance> GetAllAttendancesByUserId(Guid userId);
        public IEnumerable<Attendance> GetAllAttendancesByEventId(Guid eventId);
        public Task<Attendance?> GetAttendanceById(Guid id);
        public Task<Attendance> AddAttendance(Attendance attendance);
        public Task<Attendance> UpdateAttendance(Attendance attendance);
        public Task DeleteAttendance(Guid id);
    }
}
