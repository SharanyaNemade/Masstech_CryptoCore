using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Team1_CryptoCore.Application.DTO;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Infrastructure.Data;

namespace Team1_CryptoCore.Infrastructure.Services
{
    public class CoinMarketService : ICoinGeckoMarketService
    {

        ApplicationDbContext db;
        IMapper mapper;

        public CoinMarketService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }





        //public async Task<List<CoinMarketDTO>> GetMarketCoins(int page)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("User-Agent", "MyCryptoApp/1.0");

        //        var response = await client.GetAsync(
        //            $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page={page}&price_change_percentage=1h"
        //        );

        //        var json = await response.Content.ReadAsStringAsync();

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new Exception($"API Error: {response.StatusCode} - {json}");
        //        }

        //        var data = JsonSerializer.Deserialize<List<CoinMarketDTO>>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //        return data ?? new List<CoinMarketDTO>();
        //    }
        //}





        public async Task<List<CoinMarketDTO>> GetMarketCoins(int page)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "MyCryptoApp/1.0");

                var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page={page}&price_change_percentage=1h";

                //var url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=bitcoin&symbols=btc&price_change_percentage=1h";

                var response = await client.GetAsync(url);

                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API Error: {response.StatusCode} - {json}");
                }

                var data = JsonSerializer.Deserialize<List<CoinMarketDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<CoinMarketDTO>();
            }
        }
    }
}
