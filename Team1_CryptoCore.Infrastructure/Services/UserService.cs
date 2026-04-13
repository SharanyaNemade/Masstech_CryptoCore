using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Domain.Models;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        ApplicationDbContext db;
        IMapper mapper;
        IMemoryCache cache;
        JwtIService jwt;

        public UserService(ApplicationDbContext db, IMapper mapper, IMemoryCache cache, JwtIService jwt)
        {
            this.db = db;
            this.mapper = mapper;
            this.jwt = jwt;
            this.cache = cache;
        }



        public List<UserDTO> FetchAll(int page, int pageSize)
        {
            string key = $"Users_{page}_{pageSize}";

            if (!cache.TryGetValue(key, out List<UserDTO>? data))
            {
                var fetch = db.User
                .OrderBy(x => x.UserId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                data = mapper.Map<List<UserDTO>>(fetch);
                var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                cache.Set(key, data, options);

            }
            return data;
            //return mapper.Map<List<UserDTO>>(data);
        }


        //public List<UserDTO> FetchAll(int page, int pageSize)
        //{
        //    var data = db.User
        //    //var data = db.User.ToList();
        //    .Where(x => x.DeletedAt == null)
        //    .ToList();

        //    return mapper.Map<List<UserDTO>>(data);
        //}

        public UserDTO FetchById(int id)
        {
            var data = db.User
                .FirstOrDefault(x => x.UserId == id);
            //.FirstOrDefault(x => x.UserId == id && x.DeletedAt == null);

            return mapper.Map<UserDTO>(data);
        }

        public List<UserDTO> FetchByName(string name)
        {
            var data = db.User
                .Where(x => x.FullName.Contains(name))
                //.Where(x => x.FullName.Contains(name) && x.DeletedAt == null)
                .ToList();

            return mapper.Map<List<UserDTO>>(data);
        }


        public void AddUser(RegisterUserDTO dto)
        {
            dto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            var user = mapper.Map<User>(dto);
            db.User.Add(user);
            db.SaveChanges();
        }


        public User ValidateUser(string email, string password)
        {
            var user = db.User.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
                return null;

            return user;
        }

        public object Generate2FA(string email)
        {
            var user = db.User.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            var key = KeyGeneration.GenerateRandomKey(20);
            var base32 = Base32Encoding.ToString(key);

            user.TwoFactorSecret = base32;
            db.SaveChanges();

            var otpUri = $"otpauth://totp/CryptoApp:{email}?secret={base32}&issuer=CryptoApp";

            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(otpUri, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrData);
            var qrImage = qrCode.GetGraphic(10);

            return new
            {
                qrCode = qrImage
            };
        }

        public string VerifyOtpAndGenerateToken(string email, string otp)
        {
            var user = db.User.FirstOrDefault(x => x.Email == email);

            if (user == null || user.TwoFactorSecret == null)
                return null;

            var key = Base32Encoding.ToBytes(user.TwoFactorSecret);
            var totp = new Totp(key);

            bool isValid = totp.VerifyTotp(otp, out long _, VerificationWindow.RfcSpecifiedNetworkDelay);

            if (!isValid)
                return null;

            user.IsTwoFactorEnabled = true;
            db.SaveChanges();

            var jwt = new JwtService();
            return jwt.GenerateToken(user.UserId);
        }
    }
}
