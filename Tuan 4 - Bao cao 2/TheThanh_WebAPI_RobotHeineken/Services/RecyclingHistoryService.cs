using AutoMapper;
using TheThanh_WebAPI_RobotHeineken.Data;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Repository;

namespace TheThanh_WebAPI_RobotHeineken.Services
{
    public interface IRecyclingHistoryService
    {
        Task<IEnumerable<RecyclingHistoryDTO>> GetAllMachineHistory(int pageNumber = 1);
        Task<RecyclingHistoryDTO> GetMachineHistory(int code);
        Task<(bool Success, string ErrorMessage)> CreateMachineHistory(RecyclingHistoryDTO createDTO);
        Task<(bool Success, string ErrorMessage)> UpdateMachineHistory(int id, RecyclingHistoryDTO updateDTO);
        Task<(bool Success, string ErrorMessage)> DeleteMachineHistory(int id);

    }
    public class RecyclingHistoryService : IRecyclingHistoryService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public static int PAGE_SIZE { get; set; } = 3;


        public RecyclingHistoryService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateMachineHistory(RecyclingHistoryDTO createDTO)
        {
            RecyclingHistory newHistory = _mapper.Map<RecyclingHistory>(createDTO);

            RecyclingMachine machine = await _repository.RecyclingMachine.GetByIdAsync(m => m.MachineID == newHistory.MachineID);

            // khi thùng chứa sắp đầy sẽ kh cho thêm lon vào
            if (machine != null && machine.ContainerStatus != 1)
            {
                await _repository.RecyclingHistory.CreateAsync(newHistory);

                machine.TotalOtherCans += newHistory.OtherCans;
                machine.TotalHeinekenCans += newHistory.HeinekenCans;

                int sum = machine.TotalOtherCans + machine.TotalHeinekenCans;
                if (sum >= machine.Capacity)
                {
                    machine.ContainerStatus = 1;

                    // lưu lịch sử đầy thùng chứa
                    ContainerFullHistory newFullHistory = new()
                    {
                        MachineID = machine.MachineID,
                        TotalHeinekenCans = machine.TotalHeinekenCans,
                        TotalOtherCans = machine.TotalOtherCans,
                    };
                    await _repository.ContainerFullHistory.CreateAsync(newFullHistory);

                }

                await _repository.RecyclingMachine.UpdateAsync(machine);
            }
            else
            {
                return (false, "The container is almost full and cannot accept more cans");
            }

            return (true, null);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteMachineHistory(int id)
        {
            RecyclingHistory history = await _repository.RecyclingHistory.GetByIdAsync(m => m.HistoryID == id);

            if (history == null) return (false, "Machine not found");


            await _repository.RecyclingHistory.DeleteAsync(history);
            return (true, null);
        }

        public async Task<IEnumerable<RecyclingHistoryDTO>> GetAllMachineHistory(int pageNumber)
        {
            IEnumerable<RecyclingHistory> histories = await _repository.RecyclingHistory.GetAllWithPaginationAsync(null, pageNumber, PAGE_SIZE);
            return _mapper.Map<IEnumerable<RecyclingHistoryDTO>>(histories);
        }

        public async Task<RecyclingHistoryDTO> GetMachineHistory(int id)
        {
            RecyclingHistory history = await _repository.RecyclingHistory.GetByIdAsync(m => m.HistoryID == id);

            if (history == null) return null;

            return _mapper.Map<RecyclingHistoryDTO>(history);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateMachineHistory(int id, RecyclingHistoryDTO updateDTO)
        {

            RecyclingHistory history = await _repository.RecyclingHistory.GetByIdAsync(m => m.HistoryID == id);
            if (history == null)
            {
                return (false, "History not found");
            }

            _mapper.Map(updateDTO, history);
            await _repository.RecyclingHistory.UpdateAsync(history);
            return (true, null);
        }
    }
}
