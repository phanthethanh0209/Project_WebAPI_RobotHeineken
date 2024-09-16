using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerHistoryController : ControllerBase
    {
        private readonly IContainerFullHistoryService _historyService;

        public ContainerHistoryController(IContainerFullHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistory(ContainerFullHistoryDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _historyService.CreateContainerFullHistory(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _historyService.GetAllContainerFullHistory());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            (bool Success, string ErrorMessage) result = await _historyService.DeleteContainerFullHistory(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _historyService.GetAllContainerFullHistory());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachine()
        {
            return Ok(await _historyService.GetAllContainerFullHistory());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineByCode(int id)
        {
            ContainerFullHistoryDTO history = await _historyService.GetContainerFullHistory(id);
            if (history == null) return BadRequest("Not found");

            return Ok(history);
        }
    }
}
