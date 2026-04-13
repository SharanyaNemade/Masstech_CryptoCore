using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Domain.Models;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class UserSettingService : IUserSettingService
    {
        ApplicationDbContext db;
        IMapper mapper;
        public UserSettingService(ApplicationDbContext db,
        IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public UserSettingDTO GetSetting(int userId)
        {
            var setting = db.UserSettings.FirstOrDefault(x => x.UserId == userId);

            if (setting == null)
            {
                // Auto create
                setting = new UserSetting
                {
                    UserId = userId,
                    ThemeMode = false,
                    Notifications = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "SYSTEM"
                };

                db.UserSettings.Add(setting);
                db.SaveChanges();
            }

            return mapper.Map<UserSettingDTO>(setting);
        }

        public string UpdateSetting(int userId, UserSettingDTO dto)
        {
            var setting = db.UserSettings.FirstOrDefault(x => x.UserId == userId);

            if (setting == null)
                return "Setting not found";

            mapper.Map(dto, setting); // auto map update

            setting.ModifiedAt = DateTime.Now;
            setting.ModifiedBy = "USER";

            db.SaveChanges();

            return "Settings Updated";
        }
    }
}
