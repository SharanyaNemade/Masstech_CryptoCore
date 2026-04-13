using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Domain.Models;

namespace Team1_CryptoCore.Application.Interface
{
    public interface IUserService
    {
        UserDTO FetchById(int id);
        List<UserDTO> FetchAll(int page, int pageSize);
        List<UserDTO> FetchByName(string name);

        void AddUser(RegisterUserDTO e);


        // 2FA and Verify

        User ValidateUser(string email, string password);

        object Generate2FA(string email);

        string VerifyOtpAndGenerateToken(string email, string otp);
    }
}       
