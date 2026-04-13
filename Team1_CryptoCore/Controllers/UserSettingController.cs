using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        IUserSettingService service;
        public UserSettingController(IUserSettingService service)
        {
            this.service = service;

        }
        [HttpGet]
        public IActionResult GetSetting()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var data = service.GetSetting(userId);

            if (data == null)
                return NotFound("Settings not found");

            return Ok(data);
        }
        [HttpPut]
        public IActionResult UpdateSetting(UserSettingDTO dto)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            var result = service.UpdateSetting(userId, dto);

            return Ok(result);
        }

    }
}
