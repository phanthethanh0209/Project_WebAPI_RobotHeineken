using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {

        private readonly IQRCodeService _codeService;

        public QRCodeController(IQRCodeService codeService)
        {
            _codeService = codeService;
        }

        [HttpPost]
        //[CustomAuthorize("Create")]
        public async Task<IActionResult> CreateQRCode(QRCodeDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _codeService.CreateQRCode(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [CustomAuthorize("Update")]
        public async Task<IActionResult> UpdateQRCode(int id)
        {
            (bool Success, string ErrorMessage) result = await _codeService.UpdateQRCode(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [CustomAuthorize("Delete")]
        public async Task<IActionResult> DeleteQRCode(int id)
        {
            (bool Success, string ErrorMessage) result = await _codeService.DeleteQRCode(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _codeService.GetAllQRCode());
        }

        [HttpGet]
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllQRCode(int page = 1)
        {
            return Ok(await _codeService.GetAllQRCode(page));
        }

        [HttpGet("{id}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetQRCodeByCode(int id)
        {
            QRCodeDTO code = await _codeService.GetQRCode(id);
            if (code == null) return BadRequest("Not found");

            return Ok(code);
        }
    }
}
