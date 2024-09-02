using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMachine(CreateMachineDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _machineService.CreateMachineAsync(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _machineService.GetAllMachineAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, UpdateMachineDTO updateDto)
        {
            await _machineService.UpdateMachineAsync(id, updateDto);
            return Ok(await _machineService.GetAllMachineAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            await _machineService.DeleteMachineAsync(id);
            return Ok(await _machineService.GetAllMachineAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachine()
        {
            return Ok(await _machineService.GetAllMachineAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMachine(int id)
        {
            MachineDTO machine = await _machineService.GetMachineAsync(id);
            if (machine == null) return BadRequest("Not found");

            return Ok(machine);
        }
    }
}
