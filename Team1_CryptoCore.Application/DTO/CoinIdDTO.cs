using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.DTO
{
    using System.Text.Json.Serialization;

    public class CoinIdDTO
    {
        public string? Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("web_slug")]
        public string? WebSlug { get; set; }

        [JsonPropertyName("asset_platform_id")]
        public string? AssetPlatformId { get; set; }
    }


}
