using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Services;

[ApiController]
[Route("api/cron")]
public class CronController : ControllerBase
{
    ICronGeckoService service;

    public CronController(ICronGeckoService service)
    {
        this.service = service;
    }



    //[HttpGet("run/{page}")]
    //public async Task<IActionResult> Run(int page, int pageSize)
    //{
    //    await service.RunCronMarketSync(page);

    //    return Ok(new
    //    {
    //        message = "Cron executed successfully",
    //        page = page
    //    });
    //}



    [HttpGet("run/{page}")]
    public async Task<IActionResult> Run(int page, int pageSize = 10)
    {
        await service.RunCronMarketSync(page, pageSize);

        return Ok(ApiResponse<object>.SuccessResponse(
            new
            {
                page = page,
                pageSize = pageSize
            },
            "Cron executed successfully"
        ));
    }


}