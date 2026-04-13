using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class CoinMarket
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(250)]
        public string? CoinId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Symbol { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Image { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? CurrentPrice { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MarketCap { get; set; }


        [Required]
        public int? MarketCapRank { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? High24h { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? Low24h { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? PriceChange24h { get; set; }

        public DateTime? LastUpdated { get; set; }

 
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
