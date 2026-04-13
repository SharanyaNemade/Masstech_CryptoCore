using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class WalletTransaction
    {
        [Key]
        public int WalletTransactionId { get; set; }

        [Required]
        public bool WalletAction { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Amount { get; set; }


        [Required]
        public bool status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }




        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }

        public Transaction? Transaction { get; set; }


        [ForeignKey("Wallet")]
        public int WalletId { get; set; }

        public Wallet? Wallet { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }



        [ForeignKey("BankDetails")]
        public int AccountId { get; set; }

        public BankDetails BankDetails { get; set; }
    }
}
