using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinGeckoIDController : ControllerBase
    {

        ICoinGeckoID service;

        public CoinGeckoIDController(ICoinGeckoID service)
        {
            this.service = service;
        }


        [HttpGet]
        [Route("GetCoinById/{id}")]
        public async Task<IActionResult> GetCoinById(string id)
        {
            var data = await service.GetCoinById(id);
            return Ok(data);
        }
    }
}
