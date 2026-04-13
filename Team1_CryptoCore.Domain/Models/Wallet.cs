using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }


        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Balance { get; set; }

        public ICollection<WalletTransaction>?  WalletTransaction { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }



        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        
    }
}
