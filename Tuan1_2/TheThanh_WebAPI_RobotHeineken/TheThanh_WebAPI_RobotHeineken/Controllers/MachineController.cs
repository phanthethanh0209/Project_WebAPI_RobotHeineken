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
        public async Task<IActionResult> CreateMachine(CreateMachineDTO machineDto)
        {
            await _machineService.CreateMachineAsync(machineDto);
            return Ok(await _machineService.GetAllMachineAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachine()
        {
            return Ok(await _machineService.GetAllMachineAsync());
        }
    }
}
