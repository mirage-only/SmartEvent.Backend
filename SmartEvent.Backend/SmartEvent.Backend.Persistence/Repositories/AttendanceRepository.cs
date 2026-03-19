using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class AttendanceRepository(ApplicationDbContext dbContext) : IAttendanceRepository
    {
        public async Task<bool> AddAttendanceAsync(Attendance attendance)
        {
            await dbContext.Attendances.AddAsync(attendance);
            await  dbContext.SaveChangesAsync();
            return true;
        }
    }
}
