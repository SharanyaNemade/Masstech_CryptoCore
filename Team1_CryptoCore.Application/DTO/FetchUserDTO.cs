using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    public class FetchUserDTO
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(40)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }   //reference of user id

        public DateTime CreatedAt { get; set; }

        [MaxLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
