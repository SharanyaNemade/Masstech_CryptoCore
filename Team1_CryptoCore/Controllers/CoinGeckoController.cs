using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinGeckoController : ControllerBase
    {

        ICoinGeckoService service;

        public CoinGeckoController(ICoinGeckoService service)
        {
            this.service = service;
        }

        [HttpGet("GetCoins/{page}")]
        public async Task<IActionResult> GetCoins(int page)
        {
            var data = await service.GetCoinsFromAPI(page);
            return Ok(data);
        }
    }
}
