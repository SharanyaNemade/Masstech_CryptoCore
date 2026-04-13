using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    public class WishlistDTO
    {
        public int WishlistId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
