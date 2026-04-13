using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Services;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        IWalletService service;

        public WalletController(IWalletService service)
        {
            this.service = service;
        }


        // Post
        [HttpPost("Add")]
        public IActionResult Add(WalletDTO dto)
        {
            service.Add(dto);
            return Ok(new { message = "Wallet Added", data = dto });
        }


        //  Get All
        [HttpGet("GetAll")]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var response = service.GetAll(page, pageSize);
            return Ok(ApiResponse<List<WalletDTO>>.SuccessResponse(response,"Wallet Data fetched Successfully"));
        }



        // Get by Id
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);

            if (data == null)
                return NotFound("Wallet not found");

            return Ok(ApiResponse<WalletDTO>.SuccessResponse(data,"Wallet data fetch by Id"));
        }


        // Put
        [HttpPut("Update")]
        public IActionResult Update(WalletDTO dto)
        {
            service.Update(dto);
            return Ok(new { message = "Wallet Updated", data = dto });
        }


        // Delete
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new { message = "Wallet Deleted", id = id });
        }
    }
}
