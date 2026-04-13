using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// To use this :- https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd

namespace Team1_CryptoCore.Application.DTO
{


    using System.Text.Json.Serialization;

    public class CoinGeckoDTO
    {
        public string? Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }

        public string? Image { get; set; }

        [JsonPropertyName("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("market_cap_rank")]
        public int? MarketCapRank { get; set; }

        [JsonPropertyName("high_24h")]
        public decimal? High24h { get; set; }

        [JsonPropertyName("low_24h")]
        public decimal? Low24h { get; set; }

        [JsonPropertyName("price_change_24h")]
        public decimal? PriceChange24h { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}






//public class CoinGeckoDTO
//{
//        public string? id { get; set; }
//        public string? symbol { get; set; }
//        public string? name { get; set; }
//        public string? image { get; set; }
//        public float current_price { get; set; }
//        public long market_cap { get; set; }
//        public int market_cap_rank { get; set; }

//        public float high_24h { get; set; }
//        public float low_24h { get; set; }
//        public float price_change_24h { get; set; }

//        public DateTime last_updated { get; set; }
//    }