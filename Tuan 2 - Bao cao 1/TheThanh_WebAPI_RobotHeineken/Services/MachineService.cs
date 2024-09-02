using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;
using TheThanh_WebAPI_RobotHeineken.Validation;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetAllMachineAsync();
        Task<MachineDTO> GetMachineAsync(int id);
        Task<(bool Success, string ErrorMessage)> CreateMachineAsync(CreateMachineDTO machine);
        Task UpdateMachineAsync(int id, UpdateMachineDTO updateDTO);
        Task DeleteMachineAsync(int id);

    }
    public class MachineService : IMachineService
    {
        private readonly IRepositoryBase<RecyclingMachine> _repository;
        private readonly IMapper _mapper;
        private readonly MachineValidator _machineValidator;

        public MachineService(IRepositoryBase<RecyclingMachine> repository, IMapper mapper, MachineValidator machineValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _machineValidator = machineValidator;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateMachineAsync(CreateMachineDTO createDTO)
        {
            // Kiểm tra duy nhất của MachineCode (đã tồn tại => true)
            bool isMachineCodeUnique = await _repository.AnyAsync(m => m.MachineCode == createDTO.MachineCode);
            if (isMachineCodeUnique)
            {
                return (false, "Machine code must be unique");
            }

            FluentValidation.Results.ValidationResult validationResult = await _machineValidator.ValidateAsync(createDTO);

            if (!validationResult.IsValid)
                return (false, validationResult.Errors.First().ErrorMessage);

            RecyclingMachine newMachine = _mapper.Map<RecyclingMachine>(createDTO);
            await _repository.CreateAsync(newMachine);

            return (true, null);
        }

        public async Task UpdateMachineAsync(int id, UpdateMachineDTO updateDTO)
        {
            RecyclingMachine machine = await _repository.GetByIdAsync(m => m.MachineID == id);
            if (machine == null)
            {
                throw new Exception("Machine not found");
            }

            // Cập nhật các thuộc tính của đối tượng machine với các giá trị từ updateDTO.
            _mapper.Map(updateDTO, machine);
            await _repository.UpdateAsync(machine);
        }

        public async Task DeleteMachineAsync(int id)
        {
            RecyclingMachine machine = await _repository.GetByIdAsync(m => m.MachineID == id);

            if (machine == null) throw new KeyNotFoundException("Machine not found");

            await _repository.DeleteAsync(machine);
        }

        public async Task<IEnumerable<MachineDTO>> GetAllMachineAsync()
        {
            IEnumerable<RecyclingMachine> machines = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MachineDTO>>(machines);
        }

        public async Task<MachineDTO> GetMachineAsync(int id)
        {
            RecyclingMachine machine = await _repository.GetByIdAsync(m => m.MachineID == id);

            if (machine == null) throw new KeyNotFoundException("Machine not found");

            return _mapper.Map<MachineDTO>(machine);
        }
    }
}
