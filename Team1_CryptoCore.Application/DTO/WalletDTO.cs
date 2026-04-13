using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Domain.Models;

namespace Team1_CryptoCore.Application.DTO
{
    public class WalletDTO
    {
        public int WalletId { get; set; }

        public decimal Balance { get; set; }

        //public ICollection<WalletTransaction>? WalletTransaction { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
