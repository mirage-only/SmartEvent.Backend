using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class QrCodeRepository(ApplicationDbContext dbContext): IQrCodeRepository
    {
        public async Task<bool> AddQrCodeAsync(QrCode qrCode)
        {
            await dbContext.QrCodes.AddAsync(qrCode);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public Task DeleteQrCode(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QrCode> GetAllQrCodes()
        {
            throw new NotImplementedException();
        }

        public Task<QrCode?> GetQrCodeByEventId(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<QrCode?> GetQrCodeById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<QrCode> UpdateQrCode(QrCode qrCode)
        {
            throw new NotImplementedException();
        }
    }
}
