using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing.Printing;
using System.Text.Json;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Domain.Models;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class CronGeckoService : ICronGeckoService
    {
        ApplicationDbContext db;
        IMapper mapper;
        IMemoryCache cache;

        public CronGeckoService(ApplicationDbContext db, IMapper mapper, IMemoryCache cache)
        {
            this.db = db;
            this.mapper = mapper;
            this.cache = cache;
        }



        // API Call



        public async Task<List<CoinGeckoDTO>> GetCronMarketCoins(int page, int pageSize)
        {
            string key = $"CronCoins_{page}_{pageSize}";

            if (!cache.TryGetValue(key, out List<CoinGeckoDTO>? data))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "CronCryptoApp/1.0");

                    var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=50&page={page}&price_change_percentage=1h";

                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    // RATE LIMIT HANDLING
                    if ((int)response.StatusCode == 429)
                    {
                        Console.WriteLine("Rate limit hit. Skipping this cycle.");
                        return new List<CoinGeckoDTO>();
                    }

                    // ERROR HANDLING
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"API Error: {response.StatusCode}");
                        return new List<CoinGeckoDTO>();
                    }

                    var apiData = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<CoinGeckoDTO>();


                    // APPLY PAGINATION (same pattern as Wallet)
                    data = apiData
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();


                    // CACHE
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                    cache.Set(key, data, options);
                }
            }

            return data!;
        }




        //public async Task<List<CoinGeckoDTO>> GetCronMarketCoins(int page, int pageSize)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("User-Agent", "CronCryptoApp/1.0");

        //        var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=50&page={page}&price_change_percentage=1h";

        //        var response = await client.GetAsync(url);
        //        var json = await response.Content.ReadAsStringAsync();

        //        // HANDLE RATE LIMIT (VERY IMPORTANT)
        //        if ((int)response.StatusCode == 429)
        //        {
        //            Console.WriteLine("Rate limit hit. Skipping this cycle.");
        //            return new List<CoinGeckoDTO>(); // DO NOT THROW
        //        }

        //        // HANDLE OTHER ERRORS
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine($"API Error: {response.StatusCode}");
        //            return new List<CoinGeckoDTO>(); // SAFE FALLBACK
        //        }

        //        var data = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //        return data ?? new List<CoinGeckoDTO>();
        //    }
        //}




        // Cron Logic (For DB Save)
        public async Task RunCronMarketSync(int page, int pageSize)
        {
            Console.WriteLine($"[CRON START] {DateTime.Now}");

            var coins = await GetCronMarketCoins(page, pageSize);

            foreach (var coin in coins)
            {
                if (coin.Id == null) continue;

                var existing = db.Coins.FirstOrDefault(x => x.CoinId == coin.Id);

                if (existing != null)
                {
                    existing.CurrentPrice = coin.CurrentPrice ?? 0;
                    existing.PriceChange24h = coin.PriceChange24h ?? 0;
                    existing.ModifiedAt = DateTime.Now;
                }
                else
                {
                    db.Coins.Add(new CoinMarket
                    {
                        CoinId = coin.Id,
                        Symbol = coin.Symbol,
                        Name = coin.Name,
                        Image = coin.Image,
                        CurrentPrice = coin.CurrentPrice ?? 0,
                        MarketCap = coin.MarketCap ?? 0,
                        MarketCapRank = coin.MarketCapRank ?? 0,
                        High24h = coin.High24h ?? 0,
                        Low24h = coin.Low24h ?? 0,
                        PriceChange24h = coin.PriceChange24h ?? 0,
                        CreatedAt = DateTime.Now
                    });
                }
            }
            await db.SaveChangesAsync();

            Console.WriteLine($"[CRON END] {DateTime.Now}");
        }


        //public async Task<List<CoinGeckoDTO>> GetCronMarketCoins(int page)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("User-Agent", "CronCryptoApp/1.0");

        //        var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=50&page={page}&price_change_percentage=1h";

        //        var response = await client.GetAsync(url);
        //        var json = await response.Content.ReadAsStringAsync();

        //        if (!response.IsSuccessStatusCode)
        //            throw new Exception($"API Error: {response.StatusCode} - {json}");

        //        var data = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //        return data ?? new List<CoinGeckoDTO>();
        //    }
        //}

    }
}














//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Team1_CryptoCore.Application.DTO;
//using Team1_CryptoCore.Application.Interface;

//namespace Team1_CryptoCore.Infrastructure.Services
//{
//    public class CronGeckoService : ICronGeckoService
//    {
//        public async Task<List<CoinGeckoDTO>> GetCronMarketCoins(int page)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                client.DefaultRequestHeaders.Add("User-Agent", "CronCryptoApp/1.0");

//                var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page={page}&price_change_percentage=1h";

//                var response = await client.GetAsync(url);
//                var json = await response.Content.ReadAsStringAsync();

//                if (!response.IsSuccessStatusCode)
//                    throw new Exception($"API Error: {response.StatusCode} - {json}");

//                var data = JsonSerializer.Deserialize<List<CoinGeckoDTO>>(json, new JsonSerializerOptions
//                {
//                    PropertyNameCaseInsensitive = true
//                });

//                return data ?? new List<CoinGeckoDTO>();
//            }
//        }
//    }
//}
