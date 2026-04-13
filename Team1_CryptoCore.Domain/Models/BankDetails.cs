using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    [Index(nameof(AccountNumber), IsUnique = true)]
    public class BankDetails
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? BankName { get; set; }


        [Required]
        [MaxLength(30)]
        public string? AccountNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string? AccountHolderName { get; set; }

        [Required]
        [MaxLength(20)]
        public string? IFSCcode { get; set; }

        [Required]
        [MaxLength(20)]
        public string? AccountType { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
