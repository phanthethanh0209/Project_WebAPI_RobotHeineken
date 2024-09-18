using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Services
{

    public interface IContainerFullHistoryService
    {
        Task<IEnumerable<ContainerFullHistoryDTO>> GetAllContainerFullHistory();
        Task<ContainerFullHistoryDTO> GetContainerFullHistory(int code);
        Task<(bool Success, string ErrorMessage)> CreateContainerFullHistory(ContainerFullHistoryDTO createDTO);
        Task<(bool Success, string ErrorMessage)> DeleteContainerFullHistory(int id);

    }
    public class ContainerFullHistoryService : IContainerFullHistoryService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ContainerFullHistoryService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateContainerFullHistory(ContainerFullHistoryDTO createDTO)
        {
            ContainerFullHistory history = _mapper.Map<ContainerFullHistory>(createDTO);
            await _repository.ContainerFullHistory.CreateAsync(history);

            return (true, null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteContainerFullHistory(int id)
        {
            ContainerFullHistory history = await _repository.ContainerFullHistory.GetByIdAsync(m => m.ContainerID == id);

            if (history == null) return (false, "History not found");

            await _repository.ContainerFullHistory.DeleteAsync(history);
            return (true, null);
        }

        public async Task<IEnumerable<ContainerFullHistoryDTO>> GetAllContainerFullHistory()
        {
            IEnumerable<ContainerFullHistory> histories = await _repository.ContainerFullHistory.GetAllAsync();
            return _mapper.Map<IEnumerable<ContainerFullHistoryDTO>>(histories);
        }

        public async Task<ContainerFullHistoryDTO> GetContainerFullHistory(int code)
        {
            ContainerFullHistory history = await _repository.ContainerFullHistory.GetByIdAsync(m => m.ContainerID == code);

            if (history == null) return null;

            return _mapper.Map<ContainerFullHistoryDTO>(history);
        }
    }
}
