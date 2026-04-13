using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    using System.Text.Json.Serialization;

    public class CoinMarketDTO
    {
        public string? Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonPropertyName("price_change_percentage_1h_in_currency")]
        public decimal? PriceChange1h { get; set; }
    }
}
