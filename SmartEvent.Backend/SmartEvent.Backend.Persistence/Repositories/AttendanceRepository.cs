using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class AttendanceRepository() : IAttendanceRepository
    {
        public IEnumerable<Attendance> GetAllAttendances()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAllAttendancesByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Attendance> GetAllAttendancesByEventId(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<Attendance?> GetAttendanceById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Attendance> AddAttendance(Attendance attendance)
        {
            throw new NotImplementedException();
        }

        public Task<Attendance> UpdateAttendance(Attendance attendance)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAttendance(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
