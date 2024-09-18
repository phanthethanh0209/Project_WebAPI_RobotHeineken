using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    //[CustomAuthorize("YourPermissionName")]
    [Route("api/[controller]")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllLocation()
        {
            return Ok(await _locationService.GetAllLocation());
        }

        [HttpGet("{id}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            LocationDetailsDTO location = await _locationService.GetLocationById(id);
            if (location == null) return BadRequest("Not found");

            return Ok(location);
        }

        [HttpPost]
        [CustomAuthorize("Create")]
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
        [CustomAuthorize("Update")]
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
        [CustomAuthorize("Delete")]
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
