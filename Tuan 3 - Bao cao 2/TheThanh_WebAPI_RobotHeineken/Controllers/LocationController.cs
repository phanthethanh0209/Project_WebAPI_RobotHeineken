using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMachine()
        {
            return Ok(await _locationService.GetAllLocation());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationDTO createLocationDTO)
        {
            (bool Success, string ErrorMessage) result = await _locationService.CreateLocation(createLocationDTO);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _locationService.GetAllLocation());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationDTO updateLocationDTO)
        {
            (bool Success, string ErrorMessage) result = await _locationService.UpdateLocation(id, updateLocationDTO);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _locationService.GetAllLocation());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            (bool Success, string ErrorMessage) result = await _locationService.DeleteLocation(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _locationService.GetAllLocation());
        }
    }
}
