using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecyclingHistoryController : ControllerBase
    {
        private readonly IRecyclingHistoryService _historyService;

        public RecyclingHistoryController(IRecyclingHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        [CustomAuthorize("Create")]
        public async Task<IActionResult> CreateHistory(RecyclingHistoryDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _historyService.CreateMachineHistory(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _historyService.GetAllMachineHistory());
        }

        [HttpPut("{id}")]
        [CustomAuthorize("Update")]
        public async Task<IActionResult> UpdateMachine(int id, RecyclingHistoryDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _historyService.UpdateMachineHistory(id, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _historyService.GetAllMachineHistory());
        }

        [HttpDelete("{id}")]
        [CustomAuthorize("Delete")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            (bool Success, string ErrorMessage) result = await _historyService.DeleteMachineHistory(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _historyService.GetAllMachineHistory());
        }

        [HttpGet]
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllMachine(int page = 1)
        {
            return Ok(await _historyService.GetAllMachineHistory(page));
        }

        [HttpGet("{id}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetMachineByCode(int id)
        {
            RecyclingHistoryDTO history = await _historyService.GetMachineHistory(id);
            if (history == null) return BadRequest("Not found");

            return Ok(history);
        }
    }
}
