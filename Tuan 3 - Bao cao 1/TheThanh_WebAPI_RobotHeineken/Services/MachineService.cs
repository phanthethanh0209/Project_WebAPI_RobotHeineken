using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;
using TheThanh_WebAPI_RobotHeineken.Validation;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetAllMachine();
        Task<MachineDTO> GetMachine(int id);
        Task<(bool Success, string ErrorMessage)> CreateMachine(CreateMachineDTO createDTO);
        Task<(bool Success, string ErrorMessage)> UpdateMachine(int id, UpdateMachineDTO updateDTO);
        Task<(bool Success, string ErrorMessage)> DeleteMachine(int id);

    }
    public class MachineService : IMachineService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly CreateMachineValidator _createValidator;
        private readonly UpdateMachineValidator _updateValidator;

        public MachineService(IRepositoryWrapper repository, IMapper mapper, CreateMachineValidator createMachineValidator, UpdateMachineValidator updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createValidator = createMachineValidator;
            _updateValidator = updateValidator;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateMachine(CreateMachineDTO createDTO)
        {
            // Kiểm tra duy nhất của MachineCode (đã tồn tại => true)
            bool isMachineCodeUnique = await _repository.RecyclingMachine.AnyAsync(m => m.MachineCode == createDTO.MachineCode);
            if (isMachineCodeUnique)
            {
                return (false, "Machine code already exists");
            }

            FluentValidation.Results.ValidationResult validationResult = await _createValidator.ValidateAsync(createDTO);
            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            RecyclingMachine newMachine = _mapper.Map<RecyclingMachine>(createDTO);
            await _repository.RecyclingMachine.CreateAsync(newMachine);

            return (true, null);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateMachine(int id, UpdateMachineDTO updateDTO)
        {
            RecyclingMachine machine = await _repository.RecyclingMachine.GetByIdAsync(m => m.MachineID == id);
            if (machine == null)
            {
                return (false, "Machine not found");
            }

            // Kiểm tra duy nhất của MachineCode (đã tồn tại => true)
            bool isMachineCodeUnique = await _repository.RecyclingMachine.AnyAsync(m => m.MachineCode == updateDTO.MachineCode);
            if (isMachineCodeUnique)
            {
                return (false, "Machine code already exists");
            }
            // nếu máy đang vận hành mà mã máy bị sửa thì kh cho sửa
            else if (updateDTO.MachineCode != machine.MachineCode && machine.LocationID != null)
            {
                return (false, "The recycling machine is currently in operation and cannot be updated");
            }

            FluentValidation.Results.ValidationResult validationResult = await _updateValidator.ValidateAsync(updateDTO);

            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            // Cập nhật các thuộc tính của đối tượng machine với các giá trị từ updateDTO.
            _mapper.Map(updateDTO, machine);
            await _repository.RecyclingMachine.UpdateAsync(machine);
            return (true, null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteMachine(int id)
        {
            RecyclingMachine machine = await _repository.RecyclingMachine.GetByIdAsync(m => m.MachineID == id);

            if (machine == null) return (false, "Machine not found");

            if (machine.LocationID != null)
            {
                return (false, "The recycling machine is currently in operation and cannot be deleted");
            }

            await _repository.RecyclingMachine.DeleteAsync(machine);
            return (true, null);
        }

        public async Task<IEnumerable<MachineDTO>> GetAllMachine()
        {
            IEnumerable<RecyclingMachine> machines = await _repository.RecyclingMachine.GetAllAsync();
            return _mapper.Map<IEnumerable<MachineDTO>>(machines);
        }

        public async Task<MachineDTO> GetMachine(int id)
        {
            RecyclingMachine machine = await _repository.RecyclingMachine.GetByIdAsync(m => m.MachineID == id);

            if (machine == null) return null;

            return _mapper.Map<MachineDTO>(machine);
        }
    }
}
