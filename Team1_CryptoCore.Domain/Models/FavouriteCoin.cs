using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Team1_CryptoCore.Domain.Models
{
    
        public class FavouriteCoin
        {
            [Key]
            public int FavouriteId { get; set; }

            [Required]
            public int UserId { get; set; }

            [Required]
            [MaxLength(50)]
            public string? CoinId { get; set; }

            [Required]
            [MaxLength(20)]
            public string? Symbol { get; set; }

            [Required]
            [MaxLength(50)]
            public string? CoinName { get; set; }

            public DateTime AddedAt { get; set; } = DateTime.Now;

             

            [ForeignKey("UserId")]
            public User? User { get; set; }
        }
    }