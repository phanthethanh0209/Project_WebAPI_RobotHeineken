using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;
using TheThanh_WebAPI_RobotHeineken.Validation;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IGiftService
    {
        Task<IEnumerable<GiftDTO>> GetAllGift();
        Task<GiftDTO> GetGiftById(int id);
        Task<(bool Success, string ErrorMessage)> CreateGift(GiftDTO createDTO);
        Task<(bool Success, string ErrorMessage)> UpdateGift(int id, GiftDTO updateDTO);
        Task<(bool Success, string ErrorMessage)> DeleteGift(int id);
    }
    public class GiftService : IGiftService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly GiftValidator _giftValidator;

        public GiftService(IRepositoryWrapper repository, IMapper mapper, GiftValidator giftValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _giftValidator = giftValidator;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateGift(GiftDTO createDTO)
        {
            FluentValidation.Results.ValidationResult validationResult = await _giftValidator.ValidateAsync(createDTO);
            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            Gift newGift = _mapper.Map<Gift>(createDTO);
            await _repository.Gift.CreateAsync(newGift);

            return (true, null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteGift(int id)
        {
            Gift gift = await _repository.Gift.GetByIdAsync(m => m.GiftID == id);

            if (gift == null) return (false, "Gift not found");

            await _repository.Gift.DeleteAsync(gift);
            return (true, null);
        }

        public async Task<IEnumerable<GiftDTO>> GetAllGift()
        {
            IEnumerable<Gift> gifts = await _repository.Gift.GetAllAsync();
            return _mapper.Map<IEnumerable<GiftDTO>>(gifts);
        }

        public async Task<GiftDTO> GetGiftById(int id)
        {
            Gift gifts = await _repository.Gift.GetByIdAsync(m => m.GiftID == id);

            if (gifts == null) return null;

            return _mapper.Map<GiftDTO>(gifts);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateGift(int id, GiftDTO updateDTO)
        {
            Gift gift = await _repository.Gift.GetByIdAsync(m => m.GiftID == id);

            FluentValidation.Results.ValidationResult validationResult = await _giftValidator.ValidateAsync(updateDTO);

            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            // Cập nhật các thuộc tính của đối tượng machine với các giá trị từ updateDTO.
            _mapper.Map(updateDTO, gift);
            await _repository.Gift.UpdateAsync(gift);
            return (true, null);
        }
    }
}
