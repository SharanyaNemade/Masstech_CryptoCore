using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId {  get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal TransacAmount { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Currency { get; set; }

        [Required]
        [MaxLength(20)]
        public string? TransactionType { get; set; }
    

        [Required]
        [MaxLength(20)]
        public string? PaymentStatus { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PaymentMethod { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }


        public int  DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }


        [ForeignKey("CryptoCoin")]
        public int CryptoCoinId { get; set; }

        public CryptoCoin CryptoCoin { get; set; }



        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        public Wallet Wallet { get; set; }

    }
}
