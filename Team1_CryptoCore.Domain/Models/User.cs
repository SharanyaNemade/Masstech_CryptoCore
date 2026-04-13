using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? FullName { get; set; }



        [Required]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }



        [Required]
        [MaxLength(30)]
        public string? Email { get; set; }

    
        [Required]
        [MaxLength(150)]
        public string? PasswordHash { get; set; }


        [Required]            
        public bool IsActive { get; set; }=true;


        public bool IsTwoFactorEnabled { get; set; } = false;
        public string? TwoFactorSecret { get; set; }

        public UserSetting UserSetting { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }



    }
}
