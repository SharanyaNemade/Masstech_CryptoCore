using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteCoinController : ControllerBase
    {
        IFavouriteCoinService service;

        public FavouriteCoinController(IFavouriteCoinService service)
        {
            this.service = service;
        }

        // Add
        [HttpPost("AddFavourite")]
        public IActionResult Add(FavouriteCoinDTO dto)
        {
            try
            {
                service.Add(dto);
                return Ok(new
                {
                    message = "Added to favourites successfully",
                    data = dto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // Get all
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return Ok(data);
        }

        // Get by Id
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);

            if (data == null)
                return NotFound("Not found");

            return Ok(data);
        }

        // Get by user
        [HttpGet("GetByUser/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var data = service.GetByUser(userId);
            return Ok(data);
        }

        // Delete
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new { message = "Removed from favourites" });
        }
    }
}