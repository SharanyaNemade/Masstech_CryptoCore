using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinGeckoMarketController : ControllerBase
    {


        ICoinGeckoMarketService service;

        public CoinGeckoMarketController(ICoinGeckoMarketService service)
        {
            this.service = service;
        }



        [HttpGet("GetMarketCoins/{page}")]
        public async Task<IActionResult> GetMarketCoins(int page)
        {
            var data = await service.GetMarketCoins(page);
            return Ok(data);
        }


        //[HttpGet]
        //[Route("GetMarketCoins")]
        //public async Task<IActionResult> GetMarketCoins()
        //{
        //    var data = await service.GetMarketCoins();
        //    return Ok(data);
        //}
    }
}
