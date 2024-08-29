using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetAllMachineAsync();
        Task<RecyclingMachine> GetMachineAsync(int id);
        Task CreateMachineAsync(CreateMachineDTO machine);
        Task UpdateMachineAsync(RecyclingMachine machine);
        Task DeleteMachineAsync(int id);

    }
    public class MachineService : IMachineService
    {
        private readonly IRepositoryBase<RecyclingMachine> _repository;
        private readonly IMapper _mapper;

        public MachineService(IRepositoryBase<RecyclingMachine> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateMachineAsync(CreateMachineDTO machine)
        {
            RecyclingMachine newMachine = _mapper.Map<RecyclingMachine>(machine);
            await _repository.CreateAsync(newMachine);
        }

        public Task UpdateMachineAsync(RecyclingMachine machine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMachineAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MachineDTO>> GetAllMachineAsync()
        {
            IEnumerable<RecyclingMachine> machines = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MachineDTO>>(machines);
        }

        public Task<RecyclingMachine> GetMachineAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
