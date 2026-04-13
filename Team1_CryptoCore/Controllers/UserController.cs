using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Services;

namespace Team1_CryptoCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]       
    public class UserController : ControllerBase
    {
        IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }



        [HttpPost]
        [Route("AddUser")]

        public IActionResult AddUser(RegisterUserDTO e)
        {
            service.AddUser(e);
            return Ok(ApiResponse<RegisterUserDTO>.SuccessResponse(e, "User Added Successfully"));
        }


        [HttpGet]
        [Route("FetchAll")]
        public IActionResult FetchAll(int page, int pageSize)
        {
            var data = service.FetchAll(page, pageSize);
            return Ok(ApiResponse<List<UserDTO>>.SuccessResponse(data, "All Users Fetched"));
        }


        [HttpGet]
        [Route("FetchById/{id}")]
        public IActionResult FetchById(int id)
        {
            var data = service.FetchById(id);

            if(data == null)
                return NotFound("User not found");

            return Ok(data);
        }


        [HttpGet]
        [Route("FetchByName")]
        public IActionResult FetchByName(string name)
        {
            var data = service.FetchByName(name);
            return Ok(ApiResponse<List<UserDTO>>.SuccessResponse(data, "Users Fetched by Name"));

            //return Ok(data);
        }


        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = service.ValidateUser(email, password);

            if (user == null)
            {
                return Unauthorized("Invalid email or pass");
            }

            if (!user.IsTwoFactorEnabled)
            {
                var qr = service.Generate2FA(email);

                return Ok(new
                {
                    message = "Scan QR using Google Authenticator",
                    setup2FA = true,
                    qr
                });
            }

            return Ok(new
            {
                message = "Enter OTP",
                requireOTP = true
            });
        }

        [HttpPost("VerifyOtp")]
        public IActionResult VerifyOTP(string email, string otp)
        {
            var token = service.VerifyOtpAndGenerateToken(email, otp);

            if (token == null)
                return Unauthorized("Invalid OTP");

            return Ok(new
            {
                token
            });
        }
    }
}
