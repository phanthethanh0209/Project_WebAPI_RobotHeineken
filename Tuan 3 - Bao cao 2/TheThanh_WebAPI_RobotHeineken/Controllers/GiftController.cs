using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

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
        public async Task<IActionResult> GetAllGift()
        {
            return Ok(await _giftService.GetAllGift());
        }

        [HttpGet("{GiftID}")]
        public async Task<IActionResult> GetGiftById(int giftId)
        {
            GiftDTO gifts = await _giftService.GetGiftById(giftId);
            if (gifts == null) return BadRequest("Not found");

            return Ok(gifts);
        }

        [HttpPost]
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
