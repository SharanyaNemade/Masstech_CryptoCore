using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    public class WalletTransactionDTO
    {
        
        public int WalletTransactionId { get; set; }

        public bool WalletAction { get; set; }

        public decimal Amount { get; set; }

        public bool status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
