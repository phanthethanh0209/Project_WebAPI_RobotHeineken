using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }


        [HttpGet]
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllGift(int pageNumber)
        {
            return Ok(await _giftService.GetAllGift(pageNumber));
        }

        [HttpGet("{GiftID}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetGiftById(int giftId)
        {
            GiftDTO gifts = await _giftService.GetGiftById(giftId);
            if (gifts == null) return BadRequest("Not found");

            return Ok(gifts);
        }

        [HttpPost]
        [CustomAuthorize("Create")]
        public async Task<IActionResult> CreateGift(GiftDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _giftService.CreateGift(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _giftService.GetAllGift());
        }

        [HttpPut("{GiftID}")]
        [CustomAuthorize("Update")]
        public async Task<IActionResult> UpdateGift(int giftId, GiftDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _giftService.UpdateGift(giftId, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _giftService.GetAllGift());
        }

        [HttpDelete("{GiftID}")]
        [CustomAuthorize("Delete")]
        public async Task<IActionResult> DeleteGift(int giftId)
        {
            (bool Success, string ErrorMessage) result = await _giftService.DeleteGift(giftId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _giftService.GetAllGift());


        }
    }
}
