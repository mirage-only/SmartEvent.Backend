using SmartEvent.Backend.Core.Interfaces.IRepositories;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Repositories
{
    public class QrCodeRepository() : IQrCodeRepository
    {
        public Task<QrCode> AddQrCode(QrCode qrCode)
        {
            throw new NotImplementedException();
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
