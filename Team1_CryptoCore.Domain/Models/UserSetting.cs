using System.ComponentModel.DataAnnotations;

namespace Team1_CryptoCore.Domain.Models
{
    public class UserSetting
    {
        [Key]
        public int SettingId { get; set; }

        public int UserId { get; set; }

        public bool ThemeMode { get; set; }
        public bool Notifications { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }


        public User User { get; set; }
    }
}