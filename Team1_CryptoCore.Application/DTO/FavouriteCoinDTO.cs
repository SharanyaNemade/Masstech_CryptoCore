using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    public class FavouriteCoinDTO
    {
        //public int FavouriteId { get; set; }
        public int UserId { get; set; }

        public string? CoinId { get; set; }
        public string? Symbol { get; set; }
        public string? CoinName { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
