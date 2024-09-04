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
            (bool Success, string ErrorMessage) result = await _machineService.CreateMachine(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _machineService.GetAllMachine());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, UpdateMachineDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _machineService.UpdateMachine(id, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _machineService.GetAllMachine());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            (bool Success, string ErrorMessage) result = await _machineService.DeleteMachine(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _machineService.GetAllMachine());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachine()
        {
            return Ok(await _machineService.GetAllMachine());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMachine(int id)
        {
            MachineDTO machine = await _machineService.GetMachine(id);
            if (machine == null) return BadRequest("Not found");

            return Ok(machine);
        }
    }
}
