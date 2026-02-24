using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Core.Interfaces.IRepositories
{
    public interface IQrCodeRepository
    {
        public IQueryable<QrCode> GetAllQrCodes();
        public Task<QrCode?> GetQrCodeById(Guid id);
        public Task<QrCode?> GetQrCodeByEventId(Guid eventId);
        public Task<QrCode> AddQrCode (QrCode qrCode);
        public Task<QrCode> UpdateQrCode (QrCode qrCode);
        public Task DeleteQrCode (Guid id);
    }
}
