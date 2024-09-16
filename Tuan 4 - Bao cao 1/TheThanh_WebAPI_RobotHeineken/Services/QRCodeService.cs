using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IQRCodeService
    {
        Task<IEnumerable<QRCodeDTO>> GetAllQRCode();
        Task<QRCodeDTO> GetQRCode(int code);
        Task<(bool Success, string ErrorMessage)> CreateQRCode(QRCodeDTO createDTO);
        Task<(bool Success, string ErrorMessage)> UpdateQRCode(int id);
        Task<(bool Success, string ErrorMessage)> DeleteQRCode(int id);

    }
    public class QRCodeService : IQRCodeService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public QRCodeService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateQRCode(QRCodeDTO createDTO)
        {
            QRCode newQRCode = _mapper.Map<QRCode>(createDTO);
            newQRCode.StartTime = DateTime.Now;
            newQRCode.EndTime = newQRCode.StartTime.AddHours(24);

            await _repository.QRCode.CreateAsync(newQRCode);

            return (true, "The QR code has been generated.");
        }

        public Task<(bool Success, string ErrorMessage)> DeleteQRCode(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<QRCodeDTO>> GetAllQRCode()
        {
            IEnumerable<QRCode> machines = await _repository.QRCode.GetAllAsync();
            return _mapper.Map<IEnumerable<QRCodeDTO>>(machines);
        }

        public async Task<QRCodeDTO> GetQRCode(int code)
        {
            QRCode qr = await _repository.QRCode.GetByIdAsync(m => m.Code == code);

            if (qr == null) return null;

            return _mapper.Map<QRCodeDTO>(qr);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateQRCode(int id)
        {
            // hết hạn: 0, chưa sd: 1, đã sử dụng: 2
            QRCode qr = await _repository.QRCode.GetByIdAsync(m => m.Code == id);

            if (qr.EndTime < DateTime.Now) // hết hạn
            {
                qr.IsActive = 0;
                await _repository.QRCode.UpdateAsync(qr);
                return (false, "The QR code has expired");
            }
            else if (qr.IsActive == 2) // đã sd
            {
                return (false, "The QR code has already been used");
            }

            // sau khi quét qr thì update đã sd
            qr.IsActive = 2;
            qr.TimeUsed = DateTime.Now;
            await _repository.QRCode.UpdateAsync(qr);
            return (true, "Congratulations! You have successfully received your reward");
        }
    }
}
