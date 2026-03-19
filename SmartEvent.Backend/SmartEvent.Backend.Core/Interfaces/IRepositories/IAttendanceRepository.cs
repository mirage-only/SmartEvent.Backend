using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories;

public interface IAttendanceRepository
{
    public Task<bool> AddAttendanceAsync(Attendance attendance);
}
