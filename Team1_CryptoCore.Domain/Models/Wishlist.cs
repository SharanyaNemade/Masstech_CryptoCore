using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Domain.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        public int ModifiedBy { get; set; }


        public DateTime ModifiedAt { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedAt { get; set; }


        public int DeletedBy { get; set; }


        public DateTime DeletedAt { get; set; }


        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }


        [Required]
        [ForeignKey("CryptoCoin")]
        public int CryptoCoinId { get; set; }

        public CryptoCoin CryptoCoin { get; set; }
    }
}
