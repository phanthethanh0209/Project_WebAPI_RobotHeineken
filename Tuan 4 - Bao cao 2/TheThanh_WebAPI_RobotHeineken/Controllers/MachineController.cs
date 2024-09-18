using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

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
        [CustomAuthorize("Create")]
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
        [CustomAuthorize("Update")]
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
        [CustomAuthorize("Delete")]
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
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllMachine(int page = 1)
        {
            return Ok(await _machineService.GetAllMachine(page));
        }

        [HttpGet("{id}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetMachineByCode(int id)
        {
            MachineDTO machine = await _machineService.GetMachine(id);
            if (machine == null) return BadRequest("Not found");

            return Ok(machine);
        }
    }
}
