using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        ITransactionService service;

        public TransactionController(ITransactionService service)
        {
            this.service = service;
        }


        // Post
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(TransactionDTO dto)
        {
            service.Add(dto);
            return Ok(new { message = "Transaction Added", data = dto });
        }



        // Get all
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return Ok(data);
        }


        // Get by Id
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var data = service.GetById(id);

            if (data == null)
                return NotFound("Transaction not found");

            return Ok(data);
        }


        // Put
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(TransactionDTO dto)
        {
            service.Update(dto);
            return Ok(new { message = "Transaction Updated", data = dto });
        }


        // Delete
        [HttpDelete]
        [Route("GetById/{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new { message = "Transaction Deleted", id = id });
        }
    }
}
