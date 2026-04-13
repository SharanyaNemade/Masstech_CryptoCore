using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }

        public int Quantity { get; set; }

        public decimal TransacAmount { get; set; }

        public string? Currency { get; set; }

        public string? TransactionType { get; set; }

        public string? PaymentStatus { get; set; }

        public string? PaymentMethod { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
